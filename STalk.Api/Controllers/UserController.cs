using Application.DTO;
using Application.Enums;
using Application.RequestsModels;
using Application.Responses;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using EntityFramework.DbContexts;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
        private readonly IMapper mapper;

        public UserController(UserManager<User> userManager, MainDbContext appDb, IContactsServices contactsServices, IAccountServices accountServices,IMapper mapper)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.contactsServices = contactsServices;
            this.accountServices = accountServices;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("getUser")]
        public async Task<IActionResult> GetUser()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var files = await appDb.Files.Where(f => f.UserId == user.Id).ToListAsync();
            user.Files = files;

            if(user != null) 
            { 
                var userDTO = mapper.Map<UserDTO>(user);         
                return Ok(userDTO);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
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
        [Route("usernameChange")]
        public async Task<IActionResult> ChangeUsername([FromBody]UsernameChangeViewModel usernameChangeViewModel)
        {
            if (usernameChangeViewModel != null)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                AccountResponse response = await accountServices.ChangeUsernameAsync(user, usernameChangeViewModel);
                if (response.ResponseStatus == Status.Success)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        [Route("profileImageChange")]
        public async Task<IActionResult> ChangeProfileImage([FromForm]ProfileImageChangeViewModel profileImageChangeViewModel)
        {
            if(profileImageChangeViewModel != null)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                AccountResponse response = await accountServices.ChangeProfileImageAsync(user, profileImageChangeViewModel);
                if (response.ResponseStatus == Status.Success)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
       
        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsersRequest(string searchString)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            ContactsResponse response = await contactsServices.FindUsersAsync(user,searchString);
            if(response.ResponseStatus == Status.Success)
            {
                return Ok(response);
            }

            return StatusCode(StatusCodes.Status400BadRequest, response);
        }

        
        [HttpGet]
        [Route("getUsersContacts")]
        public async Task<IActionResult> GetUsersContacts()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            ContactsResponse response = await contactsServices.GetUsersContacts(user);
            if (response.ResponseStatus == Status.Success)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }

        [HttpGet]
        [Route("getUsersFriendRequests")]
        public async Task<IActionResult> GetUsersFriendsRequests()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            ContactsResponse response = await contactsServices.GetUsersFriendsRequests(user);
            if (response.ResponseStatus == Status.Success)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status400BadRequest, response);
        }

        [HttpPost]
        [Route("addToContacts")]
        public async Task<IActionResult> SendAddToContactsRequest([FromBody]StringRequest username)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user != null && !String.IsNullOrWhiteSpace(username.String))
            {
                ContactsResponse response = await contactsServices.SendAddToContactsRequest(user, username.String);
                if (response.ResponseStatus == Status.Success)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest,response);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        [Route("acceptAddToContacts")]
        public async Task<IActionResult> AcceptAddToContactsRequest([FromBody]LongRequest addToContactsRequestId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                ContactsResponse response = await contactsServices.AcceptAddToContactsRequest(user, addToContactsRequestId.Long);
                if (response.ResponseStatus == Status.Success)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest,response);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        [Route("declineAddToContacts")]
        public async Task<IActionResult> DeclineAddToContactsRequest([FromBody]LongRequest addToContactsRequestId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ContactsResponse response = await contactsServices.DeclineAddToContactsRequest(user, addToContactsRequestId.Long);
                if (response.ResponseStatus == Status.Success)
                {
                    return Ok(response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

    }
}
