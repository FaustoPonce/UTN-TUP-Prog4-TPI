using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class MemberDto
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public List<WorkoutClass> workoutClasses { get; set; } = new List<WorkoutClass>();

        public static MemberDto FromEntity(Member member)
        {
            return new MemberDto
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Password = member.Password,
                State = member.State.ToString(),
                workoutClasses = member.WorkoutClasses
            };
        }

        public static List<MemberDto> FromEntityList(List<Member> memberlist)
        {
            var members = new List<MemberDto>();
            foreach (var member in memberlist)
            {
                members.Add(FromEntity(member));
            }
            return members;
        }

    }
}
