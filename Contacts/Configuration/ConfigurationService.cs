using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApplication.Configuration
{
    public sealed class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;
        public ConfigurationService()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string? GetValue(string sectionPath) => _configuration[sectionPath];
    }
}
