using Application.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IContactsServices
    {
        Task<ContactsResponse> FindUsersAsync(string searchString); 
        Task<ContactsResponse> SendAddToContactsRequest(User userFrom,string usernameTo);
        Task<ContactsResponse> AcceptAddToContactsRequest(User user,long addToContactsRequestId);

    }
}
