using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreationAttendanceDto
    {
        public DateTime Date { get; set; }
        public int ?UserId { get; set; }

        public int ?EmployeeId { get; set; }

    }
}
