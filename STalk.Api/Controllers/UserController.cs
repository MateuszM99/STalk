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

        public UserController(UserManager<User> userManager, MainDbContext appDb, IContactsServices contactsServices)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.contactsServices = contactsServices;
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
