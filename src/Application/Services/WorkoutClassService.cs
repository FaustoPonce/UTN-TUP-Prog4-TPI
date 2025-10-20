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
    public class WorkoutClassService : IWorkoutClassService
    {
        private readonly IRepositoryBase<WorkoutClass> _workoutClassRepositoryBase;
        private readonly IWorkoutClassRepository _workoutClassRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IRepositoryBase<Employee> _employeeRepositoryBase;

        public WorkoutClassService(IRepositoryBase<WorkoutClass> workoutClassRepositoryBase, IWorkoutClassRepository workoutClassRepository, IMemberRepository memberRepository, IRepositoryBase<Employee> memberRepositoryBase)
        {
            _workoutClassRepositoryBase = workoutClassRepositoryBase;
            _workoutClassRepository = workoutClassRepository;
            _memberRepository = memberRepository;
            _employeeRepositoryBase = memberRepositoryBase;
        }

        public WorkoutClass Create(CreationWorkoutClassDto creationWorkoutClassDto)
        {
            if (_employeeRepositoryBase.GetById(creationWorkoutClassDto.EmployeeId) == null)
            {
                throw new ValidationException($"No se encontro un empleado con id {creationWorkoutClassDto.EmployeeId} para asociar la clase.");
            }
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
                    if (member == null)
                    {
                        throw new NotFoundException($"No se encontro un miembro con id {id} en relacion con esta clase");
                    }
                    members.Add(member);
                }
                newWorkoutClass.Members = members;
            }
            return _workoutClassRepositoryBase.create(newWorkoutClass);
        }

        public void Delete(int id)
        {
            var WorkoutClassToDelete = _workoutClassRepositoryBase.GetById(id);
            if (WorkoutClassToDelete == null)
            {
                throw new NotFoundException($"No existe una clase con id {id}");
            }
            _workoutClassRepositoryBase.Delete(WorkoutClassToDelete);
        }

        public List<WorkoutClassDto> GetAllWorkoutClass()
        {
            var workoutClasss = _workoutClassRepository.GetAll();
            if (workoutClasss == null || workoutClasss.Count == 0)
            {
                throw new NotFoundException("No existen clases todavia");
            }
            var workoutClassDtos = WorkoutClassDto.FromEntityList(workoutClasss);
            return workoutClassDtos;

        }

        public WorkoutClassDto GetById(int id)
        {
            var workoutClass = _workoutClassRepository.GetById(id);
            if (workoutClass == null)
            {
                throw new NotFoundException($"No existe una clase con id {id}");
            }
            var workoutClassDto = WorkoutClassDto.FromEntity(workoutClass);
            return workoutClassDto;
        }

        public void Update(int id, CreationWorkoutClassDto creationWorkoutClassDto)
        {
            var workoutClassToUpdate = _workoutClassRepositoryBase.GetById(id);
            
            if (workoutClassToUpdate != null)
            {
                throw new NotFoundException($"No existe una clase con id {id}");
            }
            workoutClassToUpdate.Name = creationWorkoutClassDto.Name;
            workoutClassToUpdate.Description = creationWorkoutClassDto.Description;
            workoutClassToUpdate.Schedule = creationWorkoutClassDto.Schedule;
            workoutClassToUpdate.EmployeeId = creationWorkoutClassDto.EmployeeId;
            _workoutClassRepositoryBase.Update(workoutClassToUpdate);
        }
    }
}
