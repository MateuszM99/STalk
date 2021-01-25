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
        Task AddMessageToDb(long conversationId, string userSenderId, string message, long? fileId);
        Task<string> GetUserConnectionId(string userId);
        List<MessageDTO> GetAllUserMessages(string userId);
        int GetConversationCount(string userId);
        Task RemoveUserConnectionId(string connectionId);
        List<ConversationDTO> GetConversations(string userId);
        Task<ConversationDTO> GetConversationOfUsers(string userId1, string userId2);
        List<string> GetConversationUsers(long conversationId);
        Task<List<MessageDTO>> GetConversationMessages(long conversationId, string userId);
        long AddFileToDb(byte[] fileContent, string ext, string userId);
        Task<File> GetFileFromDb(long fileId);
        Task<string> GetConversationName(string conversationId, string userId);
        Task AddUserToConversation(long conversationId, string userName, string userId);
    }
}
