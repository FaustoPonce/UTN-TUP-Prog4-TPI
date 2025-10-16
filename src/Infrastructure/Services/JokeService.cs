using Application.Interfaces;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

        public async Task<JokeDto> GetRandomJokeAsync()
        {
            var response = await _httpClient.GetAsync("random_joke");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            //var dto = new JokeDto
            //{
            //    punchline = JsonDocument.Parse(json).RootElement.GetProperty("punchline").GetString(),
            //    setup = JsonDocument.Parse(json).RootElement.GetProperty("setup").GetString()
            //};

            var dto = JsonSerializer.Deserialize<JokeDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return dto!;
        }

        public async Task<JokeDto> GetJokeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"jokes/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<JokeDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return dto!;
        }

        public async Task<List<JokeDto>> GetRandomJokeByTypeAsync(string type)
        {
            var response = await _httpClient.GetAsync($"jokes/{type}/random");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<List<JokeDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return dto!;
        }

        public async Task<List<string>> GetJokeTypes()
        {
            var response = await _httpClient.GetFromJsonAsync<List<string>>("types");
            return response!;

        }
    }
}
