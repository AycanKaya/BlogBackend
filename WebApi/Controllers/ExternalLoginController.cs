using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WebApi.Controllers
{
    /*
     *   request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=GfymX8bRn4g2epD7CxmCU8yzUY35gryv&client_secret=pI1VLYcaAZGKPwNbduUZ_-Ij092REOu7cos3bf9CFwPKfJ87R8bYFlmzcNsrqrE5&audience=https://myapi-server.com", ParameterType.RequestBody);
     */

    [Route("api/[controller]")]
    [ApiController]
    public class ExternalLoginController :ControllerBase
    {
       

        [HttpGet("AccessToken")]
        public async Task<IActionResult> getAccessToken()
        {
            var client = new RestClient("https://dev-y-mn-g3v.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.Post.ToString());
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"kl2tZvpqSUBr5rzeWuT5BxGzq8nvhIeI\",\"client_secret\":\"OdsqIZiB70AJXHiAOh0ASd78wNfxEj1xLNr1VUs8Qw2lR9CM8KUqqyZdKBHJBWOT\",\"audience\":\"https://dev-y-mn-g3v.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            return Ok(client.Execute(request));
        }
    }
}
