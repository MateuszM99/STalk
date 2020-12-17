using System;
using System.Collections.Generic;
using System.Text;
using Application.Enums;
using Domain.Models;

namespace Application.Responses
{
    public class SignUpResponse
    {
       public Status ResponseStatus { get; set; }
       public string Message { get; set; }
        
    }
}
