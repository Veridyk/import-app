using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApplication.Configuration
{
    public interface IConfigurationService
    {
        string? GetValue(string sectionPath);
    }
}
