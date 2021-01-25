using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Helpers;
using Domain.Models;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Services.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        private readonly IChatService _chatService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(IChatService chatService, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _chatService = chatService;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task SendMessage(string conversationId, string message, long? fileId)
        {
            long convId = long.Parse(conversationId);
            string senderId = _httpContextAccessor.HttpContext.Request.Query["userId"];
            await _chatService.AddMessageToDb(convId, senderId, message, fileId);
            List<string> conversationUsers = _chatService.GetConversationUsers(convId);
            string senderConnectionId = Context.ConnectionId;
            await conversationUsers.ForEachAsync(async (x) =>
            {
                string recieverConnectionId = await _chatService.GetUserConnectionId(x);
                if(recieverConnectionId != null)
                {
                    await Clients.Client(recieverConnectionId).SendAsync("RecievePrivateMessage", convId, message, senderId);
                }
            });
            //if(recieverConnectionId == null)
            //{

            //}
            //else
            //{
            //    //Clients.Client(senderConnectionId).SendAsync("SendPrivateMessage", recieverConnectionId, message);
            //}
        }
        public async Task GetMyMessages()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Query["userId"];
            List<MessageDTO> messages = _chatService.GetAllUserMessages(userId);
            await Clients.Client(Context.ConnectionId).SendAsync("UpdateAll", messages);
        }
        public async Task GetConversationMessages(string conversationId)
        {
            if(conversationId != null && conversationId != "show")
            {
                string userId = _httpContextAccessor.HttpContext.Request.Query["userId"];
                List<MessageDTO> messages = await _chatService.GetConversationMessages(long.Parse(conversationId), userId);
                await Clients.Client(Context.ConnectionId).SendAsync("LoadConversation", messages);
            }
        }
        public async Task GetConversationsCount()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Query["userId"];
            int convoCount = _chatService.GetConversationCount(userId);
            await Clients.Client(Context.ConnectionId).SendAsync("UpdateConversationCount", convoCount);
        }
        public async Task GetConversations()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Query["userId"];
            List<ConversationDTO> conversations = _chatService.GetConversations(userId);
            await Clients.Client(Context.ConnectionId).SendAsync("UpdateConversations", conversations);
        }
        public async Task GetConversationOfUsers(string userId2)
        {
            string userId1 = _httpContextAccessor.HttpContext.Request.Query["userId"];
            ConversationDTO conversationDTO = await _chatService.GetConversationOfUsers(userId1, userId2);
            await Clients.Client(Context.ConnectionId).SendAsync("");
        }
        public async Task AddUserToConversation(string conversationId, string userName)
        {
            string userId = _httpContextAccessor.HttpContext.Request.Query["userId"];
            if(long.TryParse(conversationId, out long convId))
            {
                await _chatService.AddUserToConversation(convId, userName, userId);

                List<string> conversationUsers = _chatService.GetConversationUsers(convId);
                await conversationUsers.ForEachAsync(async (x) =>
                {
                    string recieverConnectionId = await _chatService.GetUserConnectionId(x);
                    if (recieverConnectionId != null)
                    {
                        await Clients.Client(recieverConnectionId).SendAsync("RecievePrivateMessage", "system", "New user added!");
                    }
                });
            }
        }
        public override async Task OnConnectedAsync()
        {
            //Context.QueryString
            //ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            var connectionId = Context.ConnectionId;
            string userId = _httpContextAccessor.HttpContext.Request.Query["userId"];
            User currentUser = await _userManager.FindByIdAsync(userId);
            currentUser.ConnectionId = connectionId;

            await _userManager.UpdateAsync(currentUser);

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            await _chatService.RemoveUserConnectionId(connectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
