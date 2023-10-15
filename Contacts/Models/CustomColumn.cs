using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ContactsApplication.Models
{
    public sealed record CustomColumn
    {
        public string Name { get; set; } = string.Empty;
        public object? Value { get; set; } 
    }
}
