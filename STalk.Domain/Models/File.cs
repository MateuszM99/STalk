using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class File
    {
        public long Id { get; set; }    
        public string FileExtension { get; set; }
        public byte[] FileContent { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
