using Application.Enums;
using Application.Responses;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MainDbContext appDb;
        private readonly IAuthenticationServices authServices;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender emailSender;
        private readonly IAccountServices accountServices;

        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, MainDbContext appDb, IAuthenticationServices authServices, IConfiguration configuration, IEmailSender emailSender, IAccountServices accountServices)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.appDb = appDb;
            this.authServices = authServices;
            _configuration = configuration;
            this.emailSender = emailSender;
            this.accountServices = accountServices;
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

        [HttpPost]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return StatusCode(StatusCodes.Status404NotFound, "No user found or token is invalid");

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User not found");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return Ok(new { message = "Email succesfully confirmed" });

            return StatusCode(StatusCodes.Status404NotFound, "");
        }
       
        [HttpPost]
        [Route("sendConfirm")]
        public async Task<IActionResult> SendConfirmationMail(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var baseUrl = "http://localhost:44338/api/authentication/confirmEmail";
            var confirmationLink = baseUrl + String.Format("/?userId={0}&token={1}", userId, token);           
            string message = $"Click this link to confirm your account: " + confirmationLink;

            await emailSender.SendEmailAsync(user.Email, "Confirm your account", message);

            return Ok();
        }

        [HttpPost]
        [Route("sendPasswordReset")]
        public async Task<IActionResult> SendPasswordResetLink([FromBody]string email)
        {
            AccountResponse response = await accountServices.RetrievePassword(email);

            if(response.ResponseStatus == Status.Success)
            {
                return Ok(response.Message);
            } 
            else
            {
                return StatusCode(StatusCodes.Status404NotFound,response.Message);
            }            
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel resetPasswordViewModel)
        {
            AccountResponse response = await accountServices.ResetPassword(resetPasswordViewModel);
            
            if (response.ResponseStatus == Status.Success)
            {
                return Ok(response.Message);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, response.Message);
            }
        }

    }
}
