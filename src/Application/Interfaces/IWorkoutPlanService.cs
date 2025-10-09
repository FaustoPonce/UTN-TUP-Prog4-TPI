using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWorkoutPlanService
    {
        WorkoutPlan Create(CreationWorkoutPlanDto creationWorkoutPlanDto);

        List<WorkoutPlanDto> GetAllWorkoutPlans();
        WorkoutPlanDto GetById(int id);
        void Update(int id, CreationWorkoutPlanDto creationWorkoutPlanDto);
        void Delete(int id);
    }
}
