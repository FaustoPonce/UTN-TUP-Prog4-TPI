using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? MemberId { get; set; }
        public MemberDto? Member { get; set; }
        public int? EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }

        public static AttendanceDto FromEntity(Attendance attendance)
        {
            var member = attendance.Member != null ? MemberDto.FromEntity(attendance.Member) : null;

            return new AttendanceDto
            {

                Id = attendance.Id,
                Date = attendance.Date,
                MemberId = attendance.MemberId,
                Member = member,
                EmployeeId = attendance.EmployeeId,
                Employee = EmployeeDto.FromEntity(attendance.Employee)
            };
            
        }

        public static List<AttendanceDto> FromEntityList(List<Attendance> attendancelist)
        {
            var attendances = new List<AttendanceDto>();
            foreach (var attendance in attendancelist)
            {
                attendances.Add(FromEntity(attendance));
            }
            return attendances;
        }
    }
}
