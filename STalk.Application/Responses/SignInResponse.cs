using Application.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Responses
{
    public class SignInResponse
    {
        public Status ResponseStatus { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public IList<string> UserRoles { get; set; }
        public string Token { get; set; }
    }
}
