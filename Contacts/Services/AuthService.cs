using ContactsApplication.Configuration;
using ContactsApplication.Dto;
using ContactsApplication.Http;
using ContactsApplication.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactsApplication.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IConfigurationService _configuration;
        private TokenInfo Token = new();

        public AuthService(IConfigurationService configuration)
        {
            _configuration = configuration;
        }

        public async Task<TokenInfo> GetToken() => await ValidateToken() ? Token : new TokenInfo();

        public async Task Authenticate()
        {
            string? clientId = _configuration.GetValue("Auth:ClientId");
            string? clientSecret = _configuration.GetValue("Auth:ClientSecret");
            string? endpoint = _configuration.GetValue("Auth:endpoint");

            if(string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                Console.WriteLine("Please fill ClientId and ClientSecret in configuration file.");
                return;
            }

            if(string.IsNullOrEmpty(endpoint))
            {
                Console.WriteLine("Please fill endpoint in configuration file.");
                return;
            }

            var dto = new AuthDto()
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            var data = await RequestSender.SendRequest<AuthDto, AuthResponseDto>(endpoint, dto, new Dictionary<string, string>());

            if(data != null)
            {
                Token = new TokenInfo()
                {
                    AccessToken = data.AccessToken,
                    ExpiresIn = data.ExpiresIn,
                    RefreshToken = data.RefreshToken,
                    TokenType = data.TokenType,
                    ValidTo = DateTimeOffset.Parse(data.ValidTo).UtcDateTime,
                };
            }
        }

        public async Task<bool> ValidateToken()
        {
            if(string.IsNullOrEmpty(Token.AccessToken))
            {
                Console.WriteLine("Token does not exists yet.");
                await Authenticate();
            } 
            
            if(Token.ValidTo <=  DateTimeOffset.UtcNow)
            {
                Console.WriteLine("Token expired");
                await Authenticate();
            }

            return true;
        }
    }
}
