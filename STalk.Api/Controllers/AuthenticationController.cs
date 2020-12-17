using Application.Enums;
using Application.ViewModels;
using Domain.Models;
using EntityFramework.DbContexts;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MainDbContext appDb;
        private readonly IAuthenticationServices authServices;
        private readonly IConfiguration _configuration;
        
        public AuthenticateController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, MainDbContext appDb, IAuthenticationServices authServices, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.appDb = appDb;
            this.authServices = authServices;
            _configuration = configuration;           
        }


        
        [HttpPost]
        [Route("signIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel model)
        {
            var response = await authServices.SignIn(model, userManager, roleManager);
            
            if(response.ResponseStatus == Status.Success)
            {
                return Ok(response);
            }

            return Unauthorized();
        }

        
        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var response = await authServices.SignUp(model, userManager, roleManager);
            
            if (response.ResponseStatus == Status.Success)
            {
                return Ok(response);
            }

            return StatusCode(StatusCodes.Status400BadRequest, response);
        }
    }
}
