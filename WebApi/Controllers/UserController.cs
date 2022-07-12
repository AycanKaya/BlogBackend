using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTO;

namespace WebApi.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("authenticate")]

        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            return Ok(await _accountService.Login(request));
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            return Ok(await _accountService.Register(request));
        }

        

    }
}