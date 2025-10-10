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
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Member> GetAll()
        {
            return _context.Members.Include(m => m.WorkoutClasses).ThenInclude(wc => wc.Employee).ToList();       
        }

        public Member GetById(int id)
        {
            return _context.Members.Include(m => m.WorkoutClasses).ThenInclude(wc => wc.Employee).FirstOrDefault(m => m.Id == id); ;
        }
    }
}
