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
    public class MemberService : IMemberService
    {
        private readonly IRepositoryBase<Member> _memberRepositoryBase;
        private readonly IMemberRepository _memberRepository;
        private readonly IWorkoutClassRepository _workoutClassRepository;

        public MemberService(IRepositoryBase<Member> memberRepositoryBase, IMemberRepository memberRepository, IWorkoutClassRepository workoutClassRepository)
        {
            _memberRepositoryBase = memberRepositoryBase;
            _memberRepository = memberRepository;
            _workoutClassRepository = workoutClassRepository;
        }

        public Member Create(CreationMemberDto creationMemberDto)
        {
            if (!creationMemberDto.Email.Contains("@"))
            {
                throw new ValidationException("El email no es valido. Debe contener un '@'.");
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
            return _memberRepositoryBase.create(newMember);
        }

        public void Delete(int id)
        {
            var memberToDelete = _memberRepositoryBase.GetById(id);
            if (memberToDelete == null)
            {
                throw new NotFoundException($"No existe un miembro con id {id}");
            }
            _memberRepositoryBase.Delete(memberToDelete);
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
            var memberToUpdate = _memberRepositoryBase.GetById(id);
            if (memberToUpdate == null)
            {
                throw new NotFoundException($"No existe un miembro con id {id}");
            }
            memberToUpdate.Name = creationMemberDto.Name;
            memberToUpdate.Email = creationMemberDto.Email;
            memberToUpdate.Password = creationMemberDto.Password;
            memberToUpdate.State = creationMemberDto.State;
            memberToUpdate.LastUpdate = DateTime.Now;

            _memberRepositoryBase.Update(memberToUpdate);
        }
    }
}
