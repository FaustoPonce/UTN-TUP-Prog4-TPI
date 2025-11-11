using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IWorkoutClassRepository _workoutClassRepository;

        public MemberService(IMemberRepository memberRepository, IWorkoutClassRepository workoutClassRepository)
        {
            
            _memberRepository = memberRepository;
            _workoutClassRepository = workoutClassRepository;
        }

        public Member Create(CreationMemberDto creationMemberDto)
        {
            if (string.IsNullOrWhiteSpace(creationMemberDto.Name) ||
                string.IsNullOrWhiteSpace(creationMemberDto.Email) ||
                string.IsNullOrWhiteSpace(creationMemberDto.Password))
            {
                throw new ValidationException("Falta un campo. Los campos 'name', 'email' y 'password' son obligatorios.");
            }
            if (!creationMemberDto.Email.Contains("@"))
            {
                throw new ValidationException("El email no es valido. Debe contener un '@'.");
            }
            if (!Enum.IsDefined(typeof(MemberState), creationMemberDto.State))
            {
                throw new ValidationException("El campo 'state' no es valido. Debe ser 0 (Activo), 1 (Inactivo) o 2 (En deuda).");
            }
            if (creationMemberDto.WorkoutClassesID != null && creationMemberDto.WorkoutClassesID.Any(id => id <= 0))
            {
                throw new ValidationException("La lista 'workoutClassesID' tiene un valor invalido (los IDs deben ser mayores a 0).");
            }
            var newMember = new Member
            {
                Name = creationMemberDto.Name,
                Email = creationMemberDto.Email,
                Password = creationMemberDto.Password,
                State = creationMemberDto.State,
                
            };
            if (creationMemberDto.WorkoutClassesID != null && creationMemberDto.WorkoutClassesID.Any()) 
                {   
                    var workoutClasses = new List<WorkoutClass>();
                
                    foreach (var id in creationMemberDto.WorkoutClassesID) 
                    {   
                        var workoutClass = _workoutClassRepository.GetById(id);
                        if (workoutClass == null) 
                        { 
                            throw new NotFoundException($"No se encontro una clase con id {id} en relacion con este miembro");
                    }
                        workoutClasses.Add(workoutClass);
                    }
                    newMember.WorkoutClasses = workoutClasses;
                }
            return _memberRepository.create(newMember);
        }

        public void Delete(int id)
        {
            var memberToDelete = _memberRepository.GetById(id);
            if (memberToDelete == null)
            {
                throw new NotFoundException($"No existe un miembro con id {id}");
            }
            _memberRepository.Delete(memberToDelete);
        }

        public List<MemberDto> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
            if (members == null || !members.Any())
            {
                throw new NotFoundException("No hay miembros.");
            }
            var memberDtos = MemberDto.FromEntityList(members);
            return memberDtos;
        }

        public MemberDto GetById(int id)
        {
            var member = _memberRepository.GetById(id);
            if (member == null)
            {
                throw new NotFoundException($"No existe un miembro con id {id}");
            }
            var memberDto = MemberDto.FromEntity(member);
            return memberDto;
        }

        public void Update(int id, CreationMemberDto creationMemberDto)
        {
            if (string.IsNullOrWhiteSpace(creationMemberDto.Name) ||
                string.IsNullOrWhiteSpace(creationMemberDto.Email) ||
                string.IsNullOrWhiteSpace(creationMemberDto.Password))
            {
                throw new ValidationException("Falta un campo. Los campos 'name', 'email' y 'password' son obligatorios.");
            }
            if (!creationMemberDto.Email.Contains("@"))
            {
                throw new ValidationException("El email no es valido. Debe contener un '@'.");
            }
            if (!Enum.IsDefined(typeof(MemberState), creationMemberDto.State))
            {
                throw new ValidationException("El campo 'state' no es valido. Debe ser 0 (Activo), 1 (Inactivo) o 2 (En deuda).");
            }
            if (creationMemberDto.WorkoutClassesID != null && creationMemberDto.WorkoutClassesID.Any(id => id <= 0))
            {
                throw new ValidationException("La lista 'workoutClassesID' tiene un valor invalido (los IDs deben ser mayores a 0).");
            }
            var memberToUpdate = _memberRepository.GetById(id);
            if (memberToUpdate == null)
            {
                throw new NotFoundException($"No existe un miembro con id {id}");
            }
            
            memberToUpdate.Name = creationMemberDto.Name;
            memberToUpdate.Email = creationMemberDto.Email;
            memberToUpdate.Password = creationMemberDto.Password;
            memberToUpdate.State = creationMemberDto.State;
            memberToUpdate.LastUpdate = DateTime.Now;

            if (creationMemberDto.WorkoutClassesID != null)
            {
                var ids = creationMemberDto.WorkoutClassesID.Distinct().ToList();
                var workoutclasses = new List<WorkoutClass>();

                if (ids.Any()) 
                {
                    foreach (var ID in ids) 
                    {
                        var workoutclass = _workoutClassRepository.GetById(ID);
                        if (workoutclass == null)
                        {
                            throw new NotFoundException($"no se encontro una clase con id {ID} en relacion con este miembro");
                        }
                        workoutclasses.Add(workoutclass);
                    }
                }
                memberToUpdate.WorkoutClasses = workoutclasses;
            }

            _memberRepository.Update(memberToUpdate);
        }
    }
}
