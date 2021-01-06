﻿using Application.Enums;
using Application.Responses;
using Application.ViewModels;
using Domain.Models;
using EntityFramework.DbContexts;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;       
        private readonly MainDbContext appDb;
        private readonly IContactsServices contactsServices;
        private readonly IAccountServices accountServices;

        public UserController(UserManager<User> userManager, MainDbContext appDb, IContactsServices contactsServices, IAccountServices accountServices)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.contactsServices = contactsServices;
            this.accountServices = accountServices;
        }

        [HttpPost]
        [Route("emailChange")]
        public async Task<IActionResult> RequestEmailChange([FromBody]EmailChangeViewModel emailChangeViewModel)
        {
            if (emailChangeViewModel != null)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                AccountResponse response = await accountServices.ChangeEmailAsync(user, emailChangeViewModel);
                if (response.ResponseStatus == Status.Success)
                {
                    return Ok(response.Message);
                } 
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,response.Message);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        [Route("emailChangeConfirm")]
        public async Task<IActionResult> ConfirmEmailChange(string userId,string email,string emailToken)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user != null)
            {
                IdentityResult result = await userManager.ChangeEmailAsync(user, email, emailToken);
                if (result.Succeeded)
                {
                    return Ok("Email succesfully changed");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Could not change email");
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest,"User not found");
        }

        [HttpPost]
        [Route("addToContacts")]
        public async Task<IActionResult> SendAddToContactsRequest([FromBody] string username)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user != null && !String.IsNullOrWhiteSpace(username))
            {                
                if(await contactsServices.SendAddToContactsRequest(user, username))
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        [Route("acceptAddToContacts")]
        public async Task<IActionResult> AcceptAddToContactsRequest([FromBody] long addToContactsRequestId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                if(await contactsServices.AcceptAddToContactsRequest(user, addToContactsRequestId))
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
