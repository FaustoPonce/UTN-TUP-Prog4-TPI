using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }

        public static UserDto FromEntity(User user)
        {
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedDate = user.CreatedDate,
                LastUpdate = user.LastUpdate
                
            };
            return userDto;
        }

        public static List<UserDto> FromEntityList(List<User> userlist)
        {
            var users = new List<UserDto>();
            foreach (var user in userlist)
            {
                users.Add(FromEntity(user));
            }
            return users;
        }
    }
}
