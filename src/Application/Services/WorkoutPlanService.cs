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
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IRepositoryBase<WorkoutPlan> _workoutPlanRepositoryBase;
        private readonly IRepositoryBase<Member> _memberRepositoryBase;
        private readonly IWorkoutPlanRepository _workoutPlanRepository;
        public WorkoutPlanService(IRepositoryBase<WorkoutPlan> workoutPlanRepositoryBase, IRepositoryBase<Member> memberRepositoryBase, IWorkoutPlanRepository workoutPlanRepository)
        {
            _workoutPlanRepositoryBase = workoutPlanRepositoryBase;
            _memberRepositoryBase = memberRepositoryBase;
            _workoutPlanRepository = workoutPlanRepository;
        }

        public WorkoutPlan Create(CreationWorkoutPlanDto creationWorkoutPlanDto)
        {
            var member = _memberRepositoryBase.GetById(creationWorkoutPlanDto.MemberId);
            if (member == null)
            {
                throw new ArgumentException($"Member with ID {creationWorkoutPlanDto.MemberId} does not exist.");
            }
            var newWorkoutPlan = new WorkoutPlan
            {
                Name = creationWorkoutPlanDto.Name,
                Description = creationWorkoutPlanDto.Description,
                Price = creationWorkoutPlanDto.Price,
                MemberId = creationWorkoutPlanDto.MemberId,
                Member = member
            };
            return _workoutPlanRepositoryBase.create(newWorkoutPlan);
        }

        public void Delete(int id)
        {   
            var workoutPlanToDelete = _workoutPlanRepositoryBase.GetById(id);
            if (workoutPlanToDelete != null)
            {
                _workoutPlanRepositoryBase.Delete(workoutPlanToDelete); 
            }
            
        }

        public List<WorkoutPlanDto> GetAllWorkoutPlans()
        {
            var workoutPlans = _workoutPlanRepository.GetAll();
            var workoutPlanDtos = WorkoutPlanDto.FromEntityList(workoutPlans);
            return workoutPlanDtos;
        }

        public WorkoutPlanDto GetById(int id)
        {
            var workoutPlan = _workoutPlanRepository.GetById(id);
            if (workoutPlan == null)
            {
                return null;
            }
            var workoutPlanDto = WorkoutPlanDto.FromEntity(workoutPlan);
            return workoutPlanDto;

        }

        public void Update(int id, CreationWorkoutPlanDto creationWorkoutPlanDto)
        {
            var workoutPlanToUpdate = _workoutPlanRepositoryBase.GetById(id);
            var member = _memberRepositoryBase.GetById(creationWorkoutPlanDto.MemberId);
            if (member == null)
            {
                throw new ArgumentException($"Member with ID {creationWorkoutPlanDto.MemberId} does not exist.");
            }
            if (workoutPlanToUpdate != null)
            {
            workoutPlanToUpdate.Name = creationWorkoutPlanDto.Name;
            workoutPlanToUpdate.Description = creationWorkoutPlanDto.Description;
            workoutPlanToUpdate.Price = creationWorkoutPlanDto.Price;
            workoutPlanToUpdate.MemberId = creationWorkoutPlanDto.MemberId;
            workoutPlanToUpdate.Member = member;
            _workoutPlanRepositoryBase.Update(workoutPlanToUpdate);
            }
        }
    }
}
