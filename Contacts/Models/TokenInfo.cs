using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApplication.Models
{
    public sealed record TokenInfo
    {
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public DateTime ValidTo { get; set; }
        public string TokenType { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
