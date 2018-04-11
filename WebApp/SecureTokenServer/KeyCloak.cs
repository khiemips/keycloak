using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.SecureTokenServer
{
    public class KeyCloak
    {
        static IConfiguration Configuration { get; set; }

        public KeyCloak(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static async Task<string> GenarateToken(LoginViewModel login)
        {
            var token = await GetTokenFromKeyCloak(login.Username, login.Password);

            return token;
        }

        private static async Task<string> GetTokenFromKeyCloak(string username, string password)
        {
            var token = string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var tokenProvider = Configuration["Jwt:token_url"];
                    var clientId = Configuration["Jwt:client_id"];
                    var grantType = "password";
                    httpClient.BaseAddress = new Uri(tokenProvider);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));

                    var data = new FormUrlEncodedContent(
                        new[]
                        {
                            new KeyValuePair<string, string>("client_id", clientId),
                            new KeyValuePair<string, string>("grant_type", grantType),
                            new KeyValuePair<string, string>("username", username),
                            new KeyValuePair<string, string>("password", password)
                        });


                    HttpResponseMessage response = await httpClient.PostAsync("", data);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var tokenInfo = JsonConvert.DeserializeObject<dynamic>(result);

                        token = tokenInfo.access_token;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            return token;
        }
    }
}
