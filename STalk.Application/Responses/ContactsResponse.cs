using Application.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Responses
{
    public class ContactsResponse
    {
        public List<User> Users { get; set; }
        public List<AddToContactRequest> AddToContactRequests { get; set; }
        public string Message { get; set; }
        public Status ResponseStatus { get; set; }
    }
}
