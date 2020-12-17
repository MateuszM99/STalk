using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public long FileAvatarId { get; set; }       
        public virtual ContactList ContactList { get; set; }
       
        public virtual List<File> Files { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual List<Conversation> Conversations { get; set; }
        public virtual List<UserConversation> UserConversations { get; set; }
    }
}
