using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTO;
using Application.Wrappers;
using System.Net;
using WebApi.Model;


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
        public async Task<LoginResponseModel> Login(AuthenticationRequest request)
        {
            var user = await _accountService.Login(request, GenerateIPAddress());
            var result = new LoginResponseModel(user);
            result.StatusCode = 200;
            result.Message = "Logged in";
            result.Succeeded = true;
            return result;

            
            
        }

        [HttpGet("isValid")]
        public  async Task<ResponseBase> isLogged() { 
            var token= HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var isSuccedd =  _accountService.isLogged(token);
            var result = new ResponseBase();
            result.Succeeded = isSuccedd;
            if (isSuccedd)
            {
                result.Message = "Successfull";
                result.StatusCode = 200;
            }
            else
            {
               result.StatusCode= (int)HttpStatusCode.Unauthorized;
                result.Message = "İnvalid Token";
                result.Error = "Unauthorized";
            }             
            return result;
            
        }



        [HttpPost]
        [Route("register")]
        public async Task<ResponseBase> Register(RegisterRequest request)
        {
            var isSuccedd = await _accountService.Register(request);
            var response = new ResponseBase();
            if (isSuccedd)
            {
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "User registered . ";
                response.Error = "No error";
                    
            }
            return response;
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<ResponseBase> ResetPassword(ResetPasswordDTO request)
        {
            var response = new ResponseBase();
            var isSucced = await _accountService.ResetPassword(request);
            if (isSucced)
            {
                response.Succeeded = isSucced;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Password Change Successful. ";
                response.Error = "No error";
            }
            return response;
        }
        


        [HttpPost]
        [Route("UserInfo")]
        public async Task<UserInfoResponseModel> UserInfo(UserInfoDTO userInfoDTO)
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var userInfo = await _accountService.SettingUserInfo(userInfoDTO, token);
            return new UserInfoResponseModel(userInfo,true, "User info edited.",200);
            
            
        }
        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<UserInfoResponseModel> GetUserInfo()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var userInfo = await _accountService.GetUserInfoAsync(token);
            return new UserInfoResponseModel(userInfo,true, "User Info ready",200);
            

        }
        [HttpGet]
        [Route("GetAllUserInfo")]
        public async Task<AllUserInfoResponseModel> GetAllUserInfo()
        {
            var userInfos = await _accountService.GetAllUserInfo();
            return  new AllUserInfoResponseModel() {
                Succeeded = true,
                StatusCode = 200,
                Message = "Successful !",
                Error = "No Errors",
                userInfos = userInfos
            };
        }


        [HttpGet]
        [Route("GetCurrentUserRole")]
        public async Task<UserRoleResponseModel> GetCurrentUserRole()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString();
            var userRole = await _accountService.GetCurrentUserRole(token);
            return new UserRoleResponseModel()
            {
                Role = userRole,
                Succeeded = true,
                StatusCode = 200,
                Error = "No error",
                Message = "This user role : "+userRole,
            };
        }
       
        [HttpGet]
        [Route("GetAccountLevel")]
        public async Task<AccountLevelResponseModel> GetUserLevel()
        {
            var accountLevel = _accountService.GetUserLevel(GetToken());
            return new AccountLevelResponseModel(accountLevel, true, "here", 2000);

        }
        private string GetToken()
        {
            return HttpContext.Request.Headers.Authorization.ToString();
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