using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Password { get; set; }
        public string UserId { get; set; }
        public string ResetToken { get; set; }
    }
}
