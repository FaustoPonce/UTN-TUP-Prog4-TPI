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
    public class WorkoutClassRepository : RepositoryBase<WorkoutClass>, IWorkoutClassRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkoutClassRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override List<WorkoutClass> GetAll()
        {
            return _context.WorkoutClasses.Include(wc => wc.Employee).Include(wc => wc.Members).ToList();
        }

        public override WorkoutClass GetById(int id)
        {
            return _context.WorkoutClasses.Include(wc => wc.Employee).Include(wc => wc.Members).FirstOrDefault(wc => wc.Id == id);
        }
    }
}
