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
                        if (workoutClass != null) 
                        { 
                        workoutClasses.Add(workoutClass);
                        }
                    }
                    newMember.WorkoutClasses = workoutClasses;
            }
            return _memberRepositoryBase.create(newMember);
        }

        public void Delete(int id)
        {
            var memberToDelete = _memberRepositoryBase.GetById(id);
            if (memberToDelete != null)
            {
                _memberRepositoryBase.Delete(memberToDelete);
            }
        }

        public List<MemberDto> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
            var memberDtos = MemberDto.FromEntityList(members);
            return memberDtos;
        }

        public MemberDto GetById(int id)
        {
            var member = _memberRepository.GetById(id);
            if (member == null)
            {
                return null;
            }
            var memberDto = MemberDto.FromEntity(member);
            return memberDto;
        }

        public void Update(int id, CreationMemberDto creationMemberDto)
        {
            var memberToUpdate = _memberRepositoryBase.GetById(id);
            if (memberToUpdate != null)
            {
                memberToUpdate.Name = creationMemberDto.Name;
                memberToUpdate.Email = creationMemberDto.Email;
                memberToUpdate.Password = creationMemberDto.Password;
                memberToUpdate.State = creationMemberDto.State;
                memberToUpdate.LastUpdate = DateTime.Now;
                
                _memberRepositoryBase.Update(memberToUpdate);
            }
        }
    }
}
