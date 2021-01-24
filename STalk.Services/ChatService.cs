using Application.Comparers;
using Application.DTO;
using Application.Enums;
using Application.Responses;
using Application.ViewModels;
using Domain.Models;
using EntityFramework.DbContexts;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ChatService : IChatService
    {
        private readonly IEmailSender emailSender;
        private readonly MainDbContext appDb;
        private readonly UserManager<User> userManager;

        public ChatService(MainDbContext appDb, UserManager<User> userManager, IEmailSender emailSender)
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task AddMessageToDb(long conversationId, string userSenderId, string message, long? fileId)
        {
            User sender = await userManager.FindByIdAsync(userSenderId);
            //User reciever = await userManager.FindByIdAsync(userToId);
            Conversation conversationToSend = await appDb.Conversations.Include(x => x.Messages).SingleOrDefaultAsync(x => x.Id == conversationId);
            //Conversation bothConversation = sender.Conversations?.Intersect(reciever.Conversations).SingleOrDefault(); // czy jak maja wspolna grupowa konwersacje to nie wyjebie im wlasnie tej konwersacji ? Jakby ci tu coś wywalało zmień na FirstOrDefault() zaoszczędzisz sporo czasu
            //if(bothConversation == null)
            //{
            //    if(sender.Conversations.Intersect(reciever.Conversations).Count() > 1)
            //    {
            //        throw new Exception("Something went terribly wrong!");
            //    }
            //    bothConversation = new Conversation();
            //    bothConversation.Users = new List<User>();
            //    bothConversation.Users.Add(sender);
            //    bothConversation.Users.Add(reciever);
            //    appDb.Conversations.Add(bothConversation);
            //}
            conversationToSend.Messages.Add(new Message()
            {
                Conversation = conversationToSend,
                Text = message,
                User = sender,
                FileId = fileId
            });
            appDb.SaveChanges();
        }
        public List<string> GetConversationUsers(long conversationId)
        {
            return appDb.UserConversations.Where(x => x.ConversationId == conversationId).Select(x => x.UserId).ToList();
            //Conversation conversation = appDb.Conversations.Include(x => x.Users).SingleOrDefault(x => x.Id == conversationId);
            //return conversation.Users.Select(x => x.Id).ToList();
        }
        public async Task<string> GetUserConnectionId(string userId)
        {
            User user = await userManager.FindByIdAsync(userId);
            return user.ConnectionId;
        }
        public List<MessageDTO> GetAllUserMessages(string userId)
        {
            var conversations = appDb.Users.Single(x => x.Id == userId).Conversations ?? new List<Conversation>();
            var asd = conversations.SelectMany(x => x.Messages, (x, y) => new
            {
                x.Id,
                y
            }).ToList();
            var asssd = asd.Select(x => new MessageDTO
            {
                SendByMe = x.y.UserId == userId,
                Message = x.y.Text,
                SenderId = x.y.UserId
            }).ToList();
            return asssd;
        }
        public int GetConversationCount(string userId)
        {
            var appUser = appDb.Users.Include(x => x.Conversations).Single(x => x.Id == userId);
            return appUser.Conversations?.Count() ?? 0;
        }
        public Task RemoveUserConnectionId(string connectionId)
        {
            appDb.Users.Single(x => x.ConnectionId == connectionId).ConnectionId = null;
            appDb.SaveChanges();
            return Task.CompletedTask;
        }
        public List<ConversationDTO> GetConversations(string userId)
        {
            var conversationUser = appDb.Conversations.SelectMany(x => x.Users, (x, y) => new
            {
                ConversationId = x.Id,
                User = y
            });
            var conversationIds = conversationUser.Where(x => x.User.Id == userId).Select(x => x.ConversationId);
            if(conversationIds.Any())
            {
                var conversationsX = appDb.Conversations.Include(x => x.Users).Where(x => conversationIds.Contains(x.Id)).Select(x => new { x.Users, x.Messages, x.Id }).ToList();
                var conversations = appDb.Conversations.Include(x => x.Users).Where(x => conversationIds.Contains(x.Id)).ToList();
                var conversationsSelect = conversationsX
                    .Select(x => new
                    {
                        Users = x.Users.Where(x => x.Id != userId).Select(x => x.UserName).Aggregate((i, j) => i + ", " + j),
                        Reciever = x.Users.FirstOrDefault(x => x.Id != userId).Id,
                        LastMsg = x.Messages.Any() ? x.Messages.Last().Text : "",
                        LastSendByMe = x.Messages.Any() ? x.Messages.Last().UserId == userId : false
                    });
                return conversationsSelect.Select(x => new ConversationDTO()
                {                   
                    LastSendByMe = x.LastSendByMe,
                    LastMessage = x.LastMsg,
                    RecieverId = x.Reciever,
                    RecieverName = x.Users
                }).ToList();
            }
            else
            {
                return new List<ConversationDTO>();
            }
        }

        public async Task<ConversationDTO> GetConversationOfUsers(string userId1,string userId2)
        {
            if(!String.IsNullOrWhiteSpace(userId1) && !String.IsNullOrWhiteSpace(userId2))
            {                
                var conversationsWithTwoUsersIds = appDb.Conversations
                    .Include(u => u.Users)
                    .Where(u => u.Users.Count == 2)
                    .ToList();
                    

                var user1 = await appDb.Users.Include(u => u.Conversations).Where(u => u.Id == userId1).SingleOrDefaultAsync();
                var user2 = await appDb.Users.Include(u => u.Conversations).Where(u => u.Id == userId2).SingleOrDefaultAsync();
                ConversationComparer conversationComparer = new ConversationComparer();
                Conversation conversation = user1.Conversations?.Intersect(user2.Conversations,conversationComparer).Intersect(conversationsWithTwoUsersIds,conversationComparer).FirstOrDefault();

                if (conversation == null)
                {                   
                    conversation = new Conversation();
                    conversation.Users = new List<User>();
                    conversation.Users.Add(user1);
                    conversation.Users.Add(user2);
                    appDb.Conversations.Add(conversation);
                    await appDb.SaveChangesAsync();
                }

                return new ConversationDTO
                {
                    Id = conversation.Id
               };
            } 
            else
            {
                return null;
            }
        }
        public async Task<List<MessageDTO>> GetConversationMessages(long conversationId, string userId)
        {
            var xasd = appDb.Conversations.Include(x => x.Messages).FirstOrDefault(asd => asd.Id == conversationId);
            Conversation conversation = await appDb.Conversations.Include(x => x.Messages).SingleAsync(x => x.Id == conversationId);
            return conversation.Messages.Select(x => new MessageDTO
            {
                Message = x.Text,
                SendByMe = x.UserId == userId,
                SenderId = x.UserId,
                FileId = x.FileId
            }).ToList();
        }
        public long AddFileToDb(byte[] fileContent, string ext, string userId)
        {
            Domain.Models.File file = new Domain.Models.File()
            {
                FileContent = fileContent,
                FileExtension = ext,
                UserId = userId
            };
            appDb.Files.Add(file);
            appDb.SaveChanges();
            return file.Id;
        }
        public async Task<File> GetFileFromDb(long fileId)
        {
            return await appDb.Files.SingleOrDefaultAsync(x => x.Id == fileId);
        }

    }
}
