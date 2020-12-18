using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IContactsServices
    {
        Task<bool> SendAddToContactsRequest(User userFrom,string usernameTo);
        Task<bool> AcceptAddToContactsRequest(User user,long addToContactsRequestId);

    }
}
