using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WorkoutClassService : IWorkoutClassService
    {
        private readonly IRepositoryBase<WorkoutClass> _workoutClassRepositoryBase;

        public WorkoutClassService(IRepositoryBase<WorkoutClass> workoutClassRepositoryBase)
        {
            _workoutClassRepositoryBase = workoutClassRepositoryBase;
        }

        public WorkoutClass Create(CreationWorkoutClassDto creationWorkoutClassDto)
        {
            var newWorkoutClass = new WorkoutClass
            {
                Name = creationWorkoutClassDto.Name,
                Description = creationWorkoutClassDto.Description,
                Schedule = creationWorkoutClassDto.Schedule,
                EmployeeId = creationWorkoutClassDto.EmployeeId,
                
            };
            return _workoutClassRepositoryBase.create(newWorkoutClass);
        }

        public void Delete(int id)
        {
            var WorkoutClassToDelete = _workoutClassRepositoryBase.GetById(id);
            if (WorkoutClassToDelete != null)
            {
                _workoutClassRepositoryBase.Delete(WorkoutClassToDelete);
            }
        }

        public List<WorkoutClassDto> GetAllWorkoutClass()
        {
            var workoutClasss = _workoutClassRepositoryBase.GetAll();
            var workoutClassDtos = WorkoutClassDto.FromEntityList(workoutClasss);
            return workoutClassDtos;

        }

        public WorkoutClassDto GetById(int id)
        {
            var workoutClass = _workoutClassRepositoryBase.GetById(id);
            if (workoutClass == null)
            {
                return null;
            }
            var workoutClassDto = WorkoutClassDto.FromEntity(workoutClass);
            return workoutClassDto;
        }

        public void Update(int id, CreationWorkoutClassDto creationWorkoutClassDto)
        {
            var workoutClassToUpdate = _workoutClassRepositoryBase.GetById(id);
            
            if (workoutClassToUpdate != null)
            {
                workoutClassToUpdate.Name = creationWorkoutClassDto.Name;
                workoutClassToUpdate.Description = creationWorkoutClassDto.Description;
                workoutClassToUpdate.Schedule = creationWorkoutClassDto.Schedule;
                workoutClassToUpdate.EmployeeId = creationWorkoutClassDto.EmployeeId;
                _workoutClassRepositoryBase.Update(workoutClassToUpdate);
            }
        }
    }
}
