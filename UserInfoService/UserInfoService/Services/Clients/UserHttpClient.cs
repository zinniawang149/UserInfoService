using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using UserInfoService.Models;
using Microsoft.Extensions.Configuration;

namespace UserInfoService.Services.Clients
{
    public class UserHttpClient : IUserHttpClient
    {
        private readonly HttpClient _httpClient;

        public UserHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("UserInfoEndpoint")!);
        }
        public async Task<HttpServiceResult<List<User>>> GetUserInfoAsync()
        {
            //TODO: Fix the json format
            //var result = await _httpClient.GetAsync("/sampletest");

            //if (result.StatusCode != HttpStatusCode.OK) {
            //    return new HttpServiceResult<List<User>> { 
            //        Error = new Error { 
            //            Message="The call failed"
            //        }
            //    };
            //}
            //var responseString = string.Empty;
            //if (result.Content != null)
            //{
            //    responseString = await result.Content.ReadAsStringAsync();
            //}

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // ignore case of property names
                AllowTrailingCommas = true, // ignore end commas
                NumberHandling = JsonNumberHandling.AllowReadingFromString, // allow reading numbers as strings
                ReadCommentHandling = JsonCommentHandling.Skip //Ignore comments or \r\n
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var responseString = "[{ \"id\": 53, \"first\": \"Bill\", \"last\": \"Bryson\", \"age\":23, \"gender\":\"M\" },{ \"id\": 62, \"first\": \"John\", \"last\": \"Travolta\", \"age\":54, \"gender\":\"M\" },{ \"id\": 41, \"first\": \"Frank\", \"last\": \"Zappa\", \"age\":23, \"gender\":\"M\" },{ \"id\": 31, \"first\": \"Jill\", \"last\": \"Scott\", \"age\":66, \"gender\":\"M\" },{ \"id\": 31, \"first\": \"Anna\", \"last\": \"Meredith\", \"age\":66, \"gender\":\"F\" },{ \"id\": 31, \"first\": \"Janet\", \"last\": \"Jackson\", \"age\":66, \"gender\":\"F\" }]";


            var response = JsonSerializer.Deserialize<List<User>>(responseString, options);
            return new HttpServiceResult<List<User>>
            {
                Result = response
            };
        }
    }

    public class HttpServiceResult<T> {
        public T Result { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public Error Error { get; set; }

    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }


    
}
