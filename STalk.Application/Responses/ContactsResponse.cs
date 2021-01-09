using Application.DTO;
using Application.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Responses
{
    public class ContactsResponse
    {
        public List<UserDTO> Users { get; set; }
        public List<AddToContactRequestDTO> AddToContactRequests { get; set; }
        public string Message { get; set; }
        public Status ResponseStatus { get; set; }
    }
}
