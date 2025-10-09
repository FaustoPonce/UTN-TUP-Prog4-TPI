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
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public static AttendanceDto FromEntity(Attendance attendance)
        { 
            
                return new AttendanceDto
                {
                    Id = attendance.Id,
                    Date = attendance.Date,
                    UserId = attendance.UserId,
                    User = attendance.User,
                    EmployeeId = attendance.EmployeeId,
                    Employee = attendance.Employee
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
