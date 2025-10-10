using Domain.Classes;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class WorkoutClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // schedule es una clase con otras propiedades como dia, hora de inicio, hora de fin
        public Schedule Schedule { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();

        public static WorkoutClassDto FromEntity(WorkoutClass workoutClass)
        {
            if (workoutClass == null) return null;
            var workoutClassDto = new WorkoutClassDto
            {   
                Id = workoutClass.Id,
                Name = workoutClass.Name,
                Description = workoutClass.Description,
                Schedule = workoutClass.Schedule,
                EmployeeId = workoutClass.EmployeeId,
                Employee = workoutClass.Employee,
                Members = workoutClass.Members

            };
            return workoutClassDto;

        }
        public static List<WorkoutClassDto> FromEntityList(List<WorkoutClass> workoutClassList)
        {
            var workoutClasses = new List<WorkoutClassDto>();
            foreach (var workoutClass in workoutClassList)
            {
                workoutClasses.Add(FromEntity(workoutClass));
            }
            return workoutClasses;
        }
    }
}
