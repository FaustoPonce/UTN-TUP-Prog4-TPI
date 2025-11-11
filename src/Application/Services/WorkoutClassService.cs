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
        private readonly IWorkoutClassRepository _workoutClassRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public WorkoutClassService(IWorkoutClassRepository workoutClassRepository, IMemberRepository memberRepository, IEmployeeRepository employeeRepository)
        {
            _workoutClassRepository = workoutClassRepository;
            _memberRepository = memberRepository;
            _employeeRepository = employeeRepository;
        }

        public WorkoutClass Create(CreationWorkoutClassDto creationWorkoutClassDto)
        {
            if (string.IsNullOrWhiteSpace(creationWorkoutClassDto.Name) ||
                string.IsNullOrWhiteSpace(creationWorkoutClassDto.Description))
            {
                throw new ValidationException("Falta un campo. Los campos 'name' y 'description' son obligatorios.");
            }
            if (creationWorkoutClassDto.Schedule == null)
            {
                throw new ValidationException("Falta el campo 'schedule'.");

            }
            if (creationWorkoutClassDto.Schedule.ClassDays < 0 || creationWorkoutClassDto.Schedule.ClassDays > 6)
            {
                throw new ValidationException("El campo 'classDays' no es valido. Debe estar entre 0 (Lunes) y 6 (Domingo).");
            }
            if (creationWorkoutClassDto.Schedule.StartTime <= 0 || creationWorkoutClassDto.Schedule.EndTime <= 0)
            {
                throw new ValidationException("Los campos 'startTime' y 'endTime' deben ser mayores a 0.");
            }
            if (creationWorkoutClassDto.Schedule.StartTime >= creationWorkoutClassDto.Schedule.EndTime)
            {
                throw new ValidationException("El horario de inicio debe ser anterior al horario de finalizacion.");
            }
            if (creationWorkoutClassDto.EmployeeId <= 0)
            {
                throw new ValidationException("Falta el campo 'employeeId' o su valor no es valido.");
            }
            if (creationWorkoutClassDto.IdMembers != null && creationWorkoutClassDto.IdMembers.Any(id => id <= 0))
            {
                throw new ValidationException("La lista 'idMembers' contiene un valor invalido (los IDs tienen ser mayores a 0).");
            }
            if (_employeeRepository.GetById(creationWorkoutClassDto.EmployeeId) == null)
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
            return _workoutClassRepository.create(newWorkoutClass);
        }

        public void Delete(int id)
        {
            var WorkoutClassToDelete = _workoutClassRepository.GetById(id);
            if (WorkoutClassToDelete == null)
            {
                throw new NotFoundException($"No existe una clase con id {id}");
            }
            _workoutClassRepository.Delete(WorkoutClassToDelete);
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
            if (string.IsNullOrWhiteSpace(creationWorkoutClassDto.Name) ||
                string.IsNullOrWhiteSpace(creationWorkoutClassDto.Description))
            {
                throw new ValidationException("Falta un campo. Los campos 'name' y 'description' son obligatorios.");
            }
            if (creationWorkoutClassDto.Schedule == null)
            {
                throw new ValidationException("Falta el campo 'schedule'.");

            }
            if (creationWorkoutClassDto.Schedule.ClassDays < 0 || creationWorkoutClassDto.Schedule.ClassDays > 6)
            {
                throw new ValidationException("El campo 'classDays' no es valido. Debe estar entre 0 (Lunes) y 6 (Domingo).");
            }
            if (creationWorkoutClassDto.Schedule.StartTime <= 0 || creationWorkoutClassDto.Schedule.EndTime <= 0)
            {
                throw new ValidationException("Los campos 'startTime' y 'endTime' deben ser mayores a 0.");
            }
            if (creationWorkoutClassDto.Schedule.StartTime >= creationWorkoutClassDto.Schedule.EndTime)
            {
                throw new ValidationException("El horario de inicio debe ser anterior al horario de finalizacion.");
            }
            if (creationWorkoutClassDto.EmployeeId <= 0)
            {
                throw new ValidationException("Falta el campo 'employeeId' o su valor no es valido.");
            }
            if (creationWorkoutClassDto.IdMembers != null && creationWorkoutClassDto.IdMembers.Any(id => id <= 0))
            {
                throw new ValidationException("La lista 'idMembers' contiene un valor invalido (los IDs tienen ser mayores a 0).");
            }
            if (_employeeRepository.GetById(creationWorkoutClassDto.EmployeeId) == null)
            {
                throw new ValidationException($"No se encontro un empleado con id {creationWorkoutClassDto.EmployeeId} para asociar la clase.");
            }
            var workoutClassToUpdate = _workoutClassRepository.GetById(id);
            
            if (workoutClassToUpdate == null)
            {
                throw new NotFoundException($"No existe una clase con id {id}");
            }
            workoutClassToUpdate.Name = creationWorkoutClassDto.Name;
            workoutClassToUpdate.Description = creationWorkoutClassDto.Description;
            workoutClassToUpdate.Schedule = creationWorkoutClassDto.Schedule;
            workoutClassToUpdate.EmployeeId = creationWorkoutClassDto.EmployeeId;

            if (creationWorkoutClassDto.IdMembers != null) 
            {
                var IDs = creationWorkoutClassDto.IdMembers.Distinct().ToList();
                var members = new List<Member>();

                if (IDs.Any()) 
                {
                    foreach (var memberID in IDs) 
                    {
                        var member = _memberRepository.GetById(memberID);
                        if (member == null) 
                        {
                            throw new NotFoundException($"No se encontro un miembro con id {memberID} en relacion con esta clase");
                        }
                        members.Add(member);
                    }
                }
                workoutClassToUpdate.Members = members;
            }


            _workoutClassRepository.Update(workoutClassToUpdate);
        }
    }
}
