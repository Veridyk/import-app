using ContactsApplication.Configuration;
using ContactsApplication.Dto;
using ContactsApplication.Http;
using ContactsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ContactsApplication.Services
{
    public class ContactService : IContactService
    {
        private readonly IXmlReader _xmlReader;
        private readonly IAuthService _authService;
        private readonly IConfigurationService _configuration;

        public ContactService(IXmlReader reader, IAuthService authService, IConfigurationService configuration)
        {
            _xmlReader = reader;
            _authService = authService;
            _configuration = configuration;
        }

        public IEnumerable<Contact> LoadContacts(string path) => _xmlReader.Read<Contact[]>(path) ?? Array.Empty<Contact>();

        public async Task GenerateAndImportContacts()
        {
            string? contactFilePath = _configuration.GetValue("ContactImport:ContacFiletPath");

            if (string.IsNullOrEmpty(contactFilePath))
            {
                Console.WriteLine("The path for generating contact file is not filled.");
                return;
            }

            //GenerateContactFile(contactFilePath);
            await ImportContacts(contactFilePath);
        }

        private async Task ImportContacts(string path)
        {
            string? endpoint = _configuration.GetValue("ContactImport:Endpoint");

            if (string.IsNullOrEmpty(endpoint))
            {
                Console.WriteLine("Fill the contact import endpoint in configuration.");
                return;
            }

            string? chunkSizeFromConfig = _configuration.GetValue("ContactImport:ChunkSize");
            if (!int.TryParse(chunkSizeFromConfig, out int chunkSize))
            {
                Console.WriteLine("Fill the chunkSize in configuration.");
                return;
            }

            var contacts = LoadContacts(path);
            var contactsDto = contacts.Select(x => new ContactDto()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Sex = x.Sex,
                Phone = x.Phone,
                Company = x.Company,
                CustomColumns = x.CustomColumns,
                Salutation = x.Salutation
            });

            var chunks = contactsDto.Chunk(chunkSize);
            foreach ( var chunk in chunks)
            {
                var contactListDto = new ContactListDto()
                {
                    Contacts = contactsDto,
                    MailingListIds = new int[] { 1390 },
                    ContactOverwriteMode = 1,
                    PreImportAction = 1
                };
                var token = await _authService.GetToken();

                var headers = new Dictionary<string, string>()
                {
                    {"Authorization", $"{token.TokenType} {token.AccessToken}"}
                };

                var data = await RequestSender.SendRequest<ContactListDto, ContactResponseDto>(endpoint, contactListDto, headers);
                Console.WriteLine("sent");
            }
            Console.Write("end");
        }

        private void GenerateContactFile(string path)
        {
            string? countFromConfig = _configuration.GetValue("ContactImport:CountToGenerate");

            if (!int.TryParse(countFromConfig, out int countToGenerate))
            {
                Console.WriteLine("Invalid count value in config file. Please check out the value");
                return;
            }

            var Contacts = new Contact[countToGenerate];

            for (int i = 0; i < countToGenerate; i++)
            {
                var contact = new Contact()
                {
                    FirstName = "Tester",
                    LastName = "Test",
                    Email = "example@example.com",
                    Phone = "772555469",
                    Sex = SexType.Male,
                    Salutation = "Hello",
                    Company = "Test company",
                    CustomColumns = new CustomColumn[2]
                    {
                        new CustomColumn() {
                        Name = "Test name",
                        Value = "dynamic value"
                        },
                        new CustomColumn() {
                        Name = "Test name",
                        Value = "dynamic value"
                        }
                    }
                };

                Contacts[i] = contact;
            }

            _xmlReader.Write(path, Contacts);
        }
    }
}
