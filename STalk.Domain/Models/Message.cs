using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }


        public long ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }

        /// <summary>
        /// User sending message
        /// </summary>
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public long? FileId { get; set; }
    }
}
