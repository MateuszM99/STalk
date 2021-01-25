using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ConversationDTO
    {
        public long? Id { get; set; }
        public string RecieverId { get; set; }
        public string RecieverName { get; set; }
        public string LastMessage { get; set; }
        public bool LastSendByMe { get; set; }
        public bool NewMessage { get; set; }
    }
}
