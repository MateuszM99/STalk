using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UserConversation
    {
        public long Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
        
        public long ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }
    }
}
