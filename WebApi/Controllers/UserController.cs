using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Domain.Entities;
using Persistence.Context;
using Application.Interfaces;
using Application.Model;
using Application.Interfaces;

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
       

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            return Ok(await _accountService.LoginUser(model));
            
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            return Ok(await _accountService.RegisterUser(model));
        }


    }
}
