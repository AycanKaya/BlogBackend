﻿using System.Threading.Tasks;
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
            return Ok(await _accountService.Login(request, GenerateIPAddress()));
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            return Ok(await _accountService.Register(request));
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