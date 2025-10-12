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
    public class WorkoutPlanRepository : IWorkoutPlanRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkoutPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<WorkoutPlan> GetAll()
        {
            return _context.WorkoutPlans.Include(w => w.Member).ThenInclude(m => m.WorkoutClasses).ToList();
        }

        public WorkoutPlan GetById(int id)
        {
            return _context.WorkoutPlans.Include(w => w.Member).ThenInclude(m => m.WorkoutClasses).FirstOrDefault(p => p.Id == id);
        }
    }
}
