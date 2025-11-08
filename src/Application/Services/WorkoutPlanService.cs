using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
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
            if (string.IsNullOrWhiteSpace(creationWorkoutPlanDto.Name) ||
                string.IsNullOrWhiteSpace(creationWorkoutPlanDto.Description))
            {
                throw new ValidationException("Falta un campo. Los campos 'name' y 'description' son obligatorios.");
            }
            if (creationWorkoutPlanDto.Price <= 0)
            {
                throw new ValidationException("El precio del plan de entrenamiento no puede ser negativo o 0");
            }
            if (creationWorkoutPlanDto.MemberId <= 0)
            {
                throw new ValidationException("Falta el campo 'memberId' o su valor no es valido.");
            }
            var member = _memberRepositoryBase.GetById(creationWorkoutPlanDto.MemberId);
            if (member == null)
            {
                throw new NotFoundException($"No existe un miembro con id {creationWorkoutPlanDto.MemberId}");
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
            if (workoutPlanToDelete == null)
            {
                throw new NotFoundException($"No existe un plan de entrenamiento con id {id}");
            }
            _workoutPlanRepositoryBase.Delete(workoutPlanToDelete);

        }

        public List<WorkoutPlanDto> GetAllWorkoutPlans()
        {
            var workoutPlans = _workoutPlanRepository.GetAll();
            if (workoutPlans == null || workoutPlans.Count == 0)
            {
                throw new NotFoundException("No existen planes de entrenamiento todavia");
            }
            var workoutPlanDtos = WorkoutPlanDto.FromEntityList(workoutPlans);
            return workoutPlanDtos;
        }

        public WorkoutPlanDto GetById(int id)
        {
            var workoutPlan = _workoutPlanRepository.GetById(id);
            if (workoutPlan == null)
            {
                throw new NotFoundException($"No existe un plan de entrenamiento con id {id}");
            }
            var workoutPlanDto = WorkoutPlanDto.FromEntity(workoutPlan);
            return workoutPlanDto;

        }

        public void Update(int id, CreationWorkoutPlanDto creationWorkoutPlanDto)
        {
            if (string.IsNullOrWhiteSpace(creationWorkoutPlanDto.Name) ||
                string.IsNullOrWhiteSpace(creationWorkoutPlanDto.Description))
            {
                throw new ValidationException("Falta un campo. Los campos 'name' y 'description' son obligatorios.");
            }
            if (creationWorkoutPlanDto.Price <= 0)
            {
                throw new ValidationException("El precio del plan de entrenamiento no puede ser negativo o 0");
            }
            if (creationWorkoutPlanDto.MemberId <= 0)
            {
                throw new ValidationException("Falta el campo 'memberId' o su valor no es valido.");
            }
            var workoutPlanToUpdate = _workoutPlanRepositoryBase.GetById(id);
            if (workoutPlanToUpdate == null) {
                throw new NotFoundException($"No existe un plan de entrenamiento con id {id}");
            }
            var member = _memberRepositoryBase.GetById(creationWorkoutPlanDto.MemberId);
            if (member == null)
            {
                throw new NotFoundException($"No existe un miembro con id {creationWorkoutPlanDto.MemberId}");
            }
            workoutPlanToUpdate.Name = creationWorkoutPlanDto.Name;
            workoutPlanToUpdate.Description = creationWorkoutPlanDto.Description;
            workoutPlanToUpdate.Price = creationWorkoutPlanDto.Price;
            workoutPlanToUpdate.MemberId = creationWorkoutPlanDto.MemberId;
            workoutPlanToUpdate.Member = member;
            _workoutPlanRepositoryBase.Update(workoutPlanToUpdate);
           
        }
    }
}
