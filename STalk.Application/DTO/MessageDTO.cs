using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class MessageDTO
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public bool SendByMe { get; set; }
        public string Message { get; set; }
        public long? FileId { get; set; }
    }
}
