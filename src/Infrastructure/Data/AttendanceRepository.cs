using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AttendanceRepository : RepositoryBase<Attendance>, IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override List<Attendance> GetAll()
        {
            
            return _context.Attendances.Include(a => a.Member).Include(a => a.Employee).ToList();
        }

        public override Attendance GetById(int id)
        {
            return _context.Attendances.Include(a => a.Member).Include(a => a.Employee).FirstOrDefault(a => a.Id == id);
        }
    }
}
