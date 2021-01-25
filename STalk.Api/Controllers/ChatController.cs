using Application.DTO;
using Application.ViewModels;
using Domain.Models;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace STalk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IChatService _chatService;

        public ChatController(UserManager<User> userManager, IChatService chatService)
        {
            this.userManager = userManager;
            this._chatService = chatService;
        }

        [HttpGet]
        [Route("getConversation")]
        public async Task<IActionResult> GetConversationOfUsers(string userId2)
        {
            var user1 = await userManager.GetUserAsync(HttpContext.User);
            ConversationDTO conversation = null;
            try
            {
                if (user1 != null)
                {
                    conversation = await _chatService.GetConversationOfUsers(user1.Id, userId2);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (conversation != null)
            {
                return Ok(conversation);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }
        [HttpPost]
        [Route("saveFile")]
        public long SaveAndGetFileId([FromForm] FileSendModel model)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    model.file.CopyTo(ms);
                    return _chatService.AddFileToDb(ms.ToArray(), model.file.ContentType, model.userId);
                }
            }
            catch
            {
                throw;
            }
        }
        [HttpGet]
        [Route("getFile")]
        public async Task<FileResult> GetFile(long fileId)
        {
            var file = await _chatService.GetFileFromDb(fileId);
            return File(file.FileContent, file.FileExtension);
        }
        [HttpGet]
        [Route("getChatName")]
        public async Task<IActionResult> GetConversationName(string conversationId, string userId)
        {
            try
            {
                string conversationName = await _chatService.GetConversationName(conversationId, userId);
                return Ok(conversationName);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        //[HttpGet]
        //[Route("getConversationConv")]
        //public async Task<IActionResult> GetConversation(string conversationId)
        //{
        //    var user1 = await userManager.GetUserAsync(HttpContext.User);
        //    ConversationDTO conversation = null;
        //    try
        //    {
        //        if (user1 != null)
        //        {
        //            conversation = await _chatService.GetConversationOfUsers(user1.Id, userId2);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    if (conversation != null)
        //    {
        //        return Ok(conversation);
        //    }

        //    return StatusCode(StatusCodes.Status400BadRequest);
        //}
    }
}
