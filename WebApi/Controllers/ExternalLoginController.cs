using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Model;
using System.Text.Json;
using System.Collections.Generic;

namespace WebApi.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class ExternalLoginController :ControllerBase
    {
        private readonly HttpClient _httpClient;
        public ExternalLoginController()
        {
            _httpClient=new HttpClient();

         
            _httpClient.BaseAddress = new Uri("https://dev-y-mn-g3v.us.auth0.com");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<T> Get<T>(string required) where T : ExternalLoginModel
        {


            var response = await _httpClient.GetAsync(required);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content);
        }


        [HttpPost("external-login")]
        public async Task<IActionResult> LoginWithAuth0()
        {


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", "GHex6lI47lr8Dje8xtQAK5szb32eUY8E"),
                new KeyValuePair<string, string>("client_secret", "DlgdfOPMMRgFD2LLY9YS_jF-J52pEwKlRqM8hNy-r0pnIPYTpIP_9O8CQ508v_Xh"),
                new KeyValuePair<string, string>("audience", "https://blogserver.com"),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            }) ; 
            var result = await _httpClient.PostAsync("/oauth/token", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultContent);

           
            return Ok(resultContent);
        }


    

    }
}
