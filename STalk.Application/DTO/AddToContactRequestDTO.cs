using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class AddToContactRequestDTO
    {
        public long Id { get; set; }
        public UserDTO UserFrom { get; set; }
    }
}
