using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
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
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("UserInfoUri")!);
        }
        public async Task<ServiceResult<List<User>>> GetUserInfoAsync()
        {
            //TODO: Fix the json format
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


            //var responseString = "[{ \"id\": 53, \"first\": \"Bill\", \"last\": \"Bryson\", age:\"23\", \"gender\":\"Y\" },\r\n{ \"id\": 62, \"first\": \"John\", \"last\": \"Travolta\", \"age\":54, \"gender\":\"M\" },{ \"id\": 41, \"first\": \"Frank\", \"last\": \"Zappa\", \"age\":23, \"gender\":\"M\" },{ \"id\": 31, \"first\": \"Jill\", \"last\": \"Scott\", \"age\":66, \"gender\":\"M\" },{ \"id\": 31, \"first\": \"Anna\", \"last\": \"Meredith\", \"age\":66, \"gender\":\"F\" },{ \"id\": 31, \"first\": \"Janet\", \"last\": \"Jackson\", \"age\":66, \"gender\":\"F\" },]";

            List<User> response;
            try
            {
                response = JsonConvert.DeserializeObject<List<User>>(responseString, new TolerantEnumConverter() );
            }
            catch (Exception)
            {
                var usersList = new List<User>();
                response = usersList;
            }
            
            //var response = JsonSerializer.Deserialize<List<User>>(responseString, options);
            return new ServiceResult<List<User>>
            {
                Result = response
            };
        }
    }


    
}
