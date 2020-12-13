using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Conversation
    {
        public long Id { get; set; }

        public virtual List<Message> Messages { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual List<UserConversation> UserConversations { get; set; }
    }
}
