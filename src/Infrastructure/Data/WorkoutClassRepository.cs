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
    public class WorkoutClassRepository : IWorkoutClassRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkoutClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<WorkoutClass> GetAll()
        {
            return _context.WorkoutClasses.Include(wc => wc.Employee).Include(wc => wc.Members).ToList();
        }

        public WorkoutClass GetById(int id)
        {
            return _context.WorkoutClasses.Include(wc => wc.Employee).Include(wc => wc.Members).FirstOrDefault(wc => wc.Id == id);
        }
    }
}
