using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ContactsApplication.Models
{
    public sealed record Contact
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public SexType Sex { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Salutation { get; set; } = string.Empty;
        public CustomColumn[]? CustomColumns { get; set; }
    }
}
