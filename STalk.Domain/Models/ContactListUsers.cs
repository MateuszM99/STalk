using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ContactListUser
    {
        public long Id { get; set; }

        public long ContactListId { get; set; }
        public ContactList ContactList { get; set; }
    
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
