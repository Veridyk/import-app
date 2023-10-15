using ContactsApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ContactsApplication.Http
{
    public sealed class RequestSender
    {
        public static async Task<M?> SendRequest<T, M>(string endpoint, T dto, Dictionary<string, string> headers)
        {
            using var client = new HttpClient();

            foreach(var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var response = await client.PostAsJsonAsync(endpoint, dto);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Request failed with code: {response.StatusCode}.");
                Console.WriteLine($"Reason: {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync();
            return !string.IsNullOrEmpty(content) ? await response.Content.ReadFromJsonAsync<M>() : default;
        }
    }
}
