using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RecepcionistaRepository : IRecepcionistaRepository
    {
        private readonly ApplicationDbContext _context;

        public RecepcionistaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Employee GetByEmail(string name)
        {
            return _context.Employees.FirstOrDefault(e => e.Email == name && e.Role == EmployeeRole.Recepcionista);
        }
    }
}
