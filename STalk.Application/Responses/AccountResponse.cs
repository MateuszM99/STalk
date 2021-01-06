using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Responses
{
    public class AccountResponse
    {
        public Status ResponseStatus { get; set; }
        public string Message { get; set; }
    }
}
