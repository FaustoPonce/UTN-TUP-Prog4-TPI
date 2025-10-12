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
        public List<int> WorkoutClassesId { get; set; } = new List<int>();
        public List<string> workoutClasses { get; set; } = new List<string>();

        public static MemberDto FromEntity(Member member)
        {
            var dto = new MemberDto
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Password = member.Password,
                State = member.State.ToString()
            };
            if (member.WorkoutClasses != null) 
            {
                foreach (var wc in member.WorkoutClasses) 
                { 
                    dto.WorkoutClassesId.Add(wc.Id);
                    var Workoutclassdto = WorkoutClassDto.FromEntity(wc);
                    if (Workoutclassdto != null) dto.workoutClasses.Add(Workoutclassdto.Name);
                }
            }
            return dto;
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
