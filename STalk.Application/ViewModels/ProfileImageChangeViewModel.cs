using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class ProfileImageChangeViewModel
    {
        public IFormFile profileImage { get; set; }
    }
}
