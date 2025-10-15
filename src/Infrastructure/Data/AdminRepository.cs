using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Admin GetByEmail(string email)
        {
           return _context.Admins.FirstOrDefault(a => a.Email == email);
        }
    }
}
