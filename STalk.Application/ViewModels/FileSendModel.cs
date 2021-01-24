using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModels
{
    public class FileSendModel
    {
        public IFormFile file { get; set; }
        public string userId { get; set; }
    }
}
