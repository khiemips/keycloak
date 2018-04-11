using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.SecureTokenServer
{
    public class ApiService
    {
       
        public static async Task<string> GetDataFromApi(string uri, string token)
        {
            var content = string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    httpClient.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

            return content;
        }
    }
}
