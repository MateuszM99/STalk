using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ContactList
    {
        public long Id { get; set; }
        
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<ContactListUser> Contacts { get; set; }
    }
}
