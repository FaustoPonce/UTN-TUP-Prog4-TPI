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
        private readonly IWorkoutClassRepository _workoutClassRepository;
        private readonly IMemberRepository _memberRepository;

        public WorkoutClassService(IRepositoryBase<WorkoutClass> workoutClassRepositoryBase, IWorkoutClassRepository workoutClassRepository, IMemberRepository memberRepository)
        {
            _workoutClassRepositoryBase = workoutClassRepositoryBase;
            _workoutClassRepository = workoutClassRepository;
            _memberRepository = memberRepository;
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
            if (creationWorkoutClassDto.IdMembers != null && creationWorkoutClassDto.IdMembers.Any())
            {
                var members = new List<Member>();
                foreach (var id in creationWorkoutClassDto.IdMembers)
                {
                    var member = _memberRepository.GetById(id);
                    if (member != null)
                    {
                        members.Add(member);
                    }
                }
                newWorkoutClass.Members = members;
            }
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
            var workoutClasss = _workoutClassRepository.GetAll();
            var workoutClassDtos = WorkoutClassDto.FromEntityList(workoutClasss);
            return workoutClassDtos;

        }

        public WorkoutClassDto GetById(int id)
        {
            var workoutClass = _workoutClassRepository.GetById(id);
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
