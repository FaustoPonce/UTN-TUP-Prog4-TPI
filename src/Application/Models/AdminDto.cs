using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }

        public static AdminDto FromEntity(Admin admin)
        {   
            if (admin == null) return null;
            var adminDto = new AdminDto
            {
                Id = admin.Id,
                Name = admin.Name,
                Email = admin.Email,
                CreatedDate = admin.CreatedDate,
                LastUpdate = admin.LastUpdate
            };
            return adminDto;
        }

        public static List<AdminDto> FromEntityList(List<Admin> adminList)
        {
            var admins = new List<AdminDto>();
            foreach (var admin in adminList)
            {
                admins.Add(FromEntity(admin));
            }
            return admins;
        }
    }
}
