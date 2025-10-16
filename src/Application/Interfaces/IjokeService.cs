using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IjokeService
    {
        Task<JokeDto> GetRandomJokeAsync();
        Task<JokeDto> GetJokeByIdAsync(int id);
        Task<List<JokeDto>> GetRandomJokeByTypeAsync(string type);
        Task<List<string>> GetJokeTypes();
    }
}
