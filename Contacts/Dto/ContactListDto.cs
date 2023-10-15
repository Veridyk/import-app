using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactsApplication.Dto
{
    public sealed record ContactListDto
    {
        [JsonPropertyName("contacts")]
        public IEnumerable<ContactDto>? Contacts { get; set; }
        [JsonPropertyName("mailingListIds")]
        public IEnumerable<int>? MailingListIds { get; set; }
        [JsonPropertyName("contactOverwriteMode")]
        public int ContactOverwriteMode { get; set; }
        [JsonPropertyName("preImportAction")]
        public int PreImportAction { get; set; }
    }
}
