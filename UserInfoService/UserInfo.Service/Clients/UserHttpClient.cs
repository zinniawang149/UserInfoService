using System.Net;
using UserInfo.Service.Helper;
using UserInfoService.Models;


namespace UserInfoService.Services.Clients
{
    public class UserHttpClient : IUserDataClient
    {
        private readonly HttpClient _httpClient;

        public UserHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Default");
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("USER_SOURCE_URI")!);
        }
        public async Task<ServiceResult<List<User>>> GetUserInfoAsync()
        {
            var result = await _httpClient.GetAsync("/sampletest");

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return new ServiceResult<List<User>>
                {
                    Error = new Error
                    {
                        Code = result.StatusCode.ToString(),
                        Message = "The getUserInfo calls failed"
                    }
                };
            }
            var responseString = string.Empty;
            if (result.Content != null)
            {
                responseString = await result.Content.ReadAsStringAsync();
            }
            var usersCollection = TolerantJsonConvert.DeserializeCollectionObject<User>(responseString, new TolerantEnumConverter());
            
            return new ServiceResult<List<User>>
            {
                Result = usersCollection == null? new List<User>() : usersCollection.ToList(),
            };
        }
    }


    
}
