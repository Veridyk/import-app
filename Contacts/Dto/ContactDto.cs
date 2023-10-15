using ContactsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactsApplication.Dto
{
    public sealed record ContactDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("sex")]
        public SexType Sex { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;
        [JsonPropertyName("company")]
        public string Company { get; set; } = string.Empty;
        [JsonPropertyName("salutation")]
        public string Salutation { get; set; } = string.Empty;
        [JsonPropertyName("customColumns")]
        public IEnumerable<CustomColumn>? CustomColumns { get; set; }
    }
}
