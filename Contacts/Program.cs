using ContactsApplication.Configuration;
using ContactsApplication.Models;
using ContactsApplication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
            .AddSingleton<IXmlReader, XmlReader>()
            .AddSingleton<IAuthService, AuthService>()
            .AddSingleton<IConfigurationService, ConfigurationService>()
            .AddTransient<IContactService, ContactService>()
.BuildServiceProvider();

var contactService = serviceProvider.GetService<IContactService>();

if(contactService != null)
{
    await contactService.GenerateAndImportContacts();
}

Console.ReadLine();
