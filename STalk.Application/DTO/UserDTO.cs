using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public byte[] ProfileImage { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
