using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JokeService : IjokeService
    {

        private readonly HttpClient _httpClient;

        public JokeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("APIHttpclient");
        }

        public async Task<string> GetRandomJokeAsync()
        {
            var response = await _httpClient.GetAsync("random_joke");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var joke = JsonDocument.Parse(json).RootElement.GetProperty("punchline").GetString();

            return joke!;
        }
    }
}
