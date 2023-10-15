using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactsApplication.Dto
{
    public sealed record AuthDto
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; } = string.Empty;
        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; } = string.Empty;
    }
}
