using Application.DTO;
using Application.Responses;
using Application.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IChatService
    {
        Task AddMessageToDb(string userToId, string userSenderId, string message);
        Task<string> GetUserConnectionId(string userId);
        List<MessageDTO> GetAllUserMessages(string userId);
        int GetConversationCount(string userId);
        Task RemoveUserConnectionId(string connectionId);
        List<ConversationDTO> GetConversations(string userId);
        Task<ConversationDTO> GetConversationOfUsers(string userId1, string userId2);
    }
}
