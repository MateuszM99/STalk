using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AddToContactRequest
    {
        public long Id { get; set; }
        public virtual User UserFrom { get; set; }
        public string UserFromId { get; set; }
        public string UserToId { get; set; }
        public virtual User UserTo { get; set; }
    }
}
