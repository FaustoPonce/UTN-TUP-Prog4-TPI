using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class WorkoutPlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public static WorkoutPlanDto FromEntity(WorkoutPlan workoutPlan)
        {
            if (workoutPlan == null) return null;
            var workoutPlanDto = new WorkoutPlanDto
            {
                Id = workoutPlan.Id,
                Name = workoutPlan.Name,
                Description = workoutPlan.Description,
                Price = workoutPlan.Price,
                MemberId = workoutPlan.MemberId,
                Member = workoutPlan.Member
            };
            return workoutPlanDto;
        }

        public static List<WorkoutPlanDto> FromEntityList(List<WorkoutPlan> workoutPlanList)
        {
            var workoutPlans = new List<WorkoutPlanDto>();
            foreach (var workoutPlan in workoutPlanList)
            {
                workoutPlans.Add(FromEntity(workoutPlan));
            }
            return workoutPlans;
        }
    }
}
