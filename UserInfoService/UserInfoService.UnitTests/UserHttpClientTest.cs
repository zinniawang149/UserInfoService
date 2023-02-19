using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using UserInfoService.Models;
using UserInfoService.Services.Clients;

namespace UserInfo.Service.UnitTests
{
    public class UserHttpClientTest
    {
        public UserHttpClientTest()
        {
            Environment.SetEnvironmentVariable("USER_SOURCE_URI", "https://mock.com");
        }

        [Fact(DisplayName = "Get valid status code and content from user endpoint, the GetUserInfoAsync method should return correct userList")]
        public async Task EndpointReturnValidContent_GetUserInfoAsync_ShouldReturnUserList()
        {
            // Arrange
            var expectUser = new List<User> {
                new User(){
                   Id=53,
                   First = "Bill",
                   Last = "Bryson",
                   Age = 23,
                   Gender = Gender.M
                },
                new User()
                {
                   Id=10,
                   First = "Zinnia",
                   Last = "Wang",
                   Age = 18,
                   Gender = Gender.F
                }
            };
            var endpointReturn = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectUser))
            };
            var httpClientFactoryMock = PrepareMockClientFactory(endpointReturn);
            var userHttpClient = new UserHttpClient(httpClientFactoryMock.Object);

            // Act
            var result = await userHttpClient.GetUserInfoAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectUser.Count(), result.Result.Count());
            Assert.Equal(JsonConvert.SerializeObject(expectUser), JsonConvert.SerializeObject(result.Result));
        }

        [Fact(DisplayName = "Get valid status code without content from user endpoint, the GetUserInfoAsync method should return empty list")]
        public async Task EndpointReturnEmptyContent_GetUserInfoAsync_ShouldReturnEmptyList()
        {
            // Arrange
            var endpointReturn = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            };
            var httpClientFactoryMock = PrepareMockClientFactory(endpointReturn);
            var userHttpClient = new UserHttpClient(httpClientFactoryMock.Object);

            // Act
            var result = await userHttpClient.GetUserInfoAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Result);
        }

        [Fact(DisplayName = "Get invalid status code from user endpoint, the GetUserInfoAsync method should return error")]
        public async Task EndpointReturnError_GetUserInfoAsync_ShouldReturnError()
        {
            // Arrange
            var endpointReturn = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("")
            };
            var httpClientFactoryMock = PrepareMockClientFactory(endpointReturn);
            var userHttpClient = new UserHttpClient(httpClientFactoryMock.Object);

            var serviceResult = new ServiceResult<List<User>>
            {
                Error = new Error
                {
                    Code = "InternalServerError",
                    Message = "The getUserInfo calls failed"
                }
            };

            // Act
            var result = await userHttpClient.GetUserInfoAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(JsonConvert.SerializeObject(serviceResult), JsonConvert.SerializeObject(result));
        }

        private Mock<IHttpClientFactory> PrepareMockClientFactory(HttpResponseMessage endpointReturn) {
            var handlerMock = new Mock<HttpMessageHandler>();
            
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(endpointReturn);
            var httpClient = new HttpClient(handlerMock.Object);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            return httpClientFactoryMock;
        }
    }
}