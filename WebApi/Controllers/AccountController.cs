using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTO;
using Application.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("authenticate")]

        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            return Ok(await _accountService.Login(request, GenerateIPAddress()));
            
        }

        [HttpGet("isValid")]
        public  IActionResult isLogged() { 
            var token= HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            return Ok( _accountService.isLogged(token));
        }
        


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = Ok(await _accountService.Register(request));
            return response;
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO request)
        {
            return Ok(await _accountService.ResetPassword(request));
        }



        [HttpPost]
        [Route("UserInfo")]
        public async Task<IActionResult> UserInfo(UserInfoDTO userInfoDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return Ok(await _accountService.SettingUserInfo(userInfoDTO, token));

        }
        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return Ok(await _accountService.GetUserInfoAsync(token));
        }
        [HttpGet]
        [Route("GetAllUserInfo")]
        public async Task<IActionResult> GetAllUserInfo()
        {
            return Ok(await _accountService.GetAllUserInfo());
        }


        [HttpGet]
        [Route("GetCurrentUserRole")]
        public async Task<IActionResult> GetCurrentUserRole()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            return Ok(await _accountService.GetCurrentUserRole(token));
        }


        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress == null ? "127.0.0.1" : HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }


    }
}