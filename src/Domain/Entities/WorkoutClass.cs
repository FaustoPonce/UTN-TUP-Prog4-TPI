using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkoutClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public schedule Schedule { get; set; }
        public class schedule
        {
            public int ClassDays { get; set; }
            public int StartTime { get; set; }
            public int EndTime { get; set; }
        }
        public int employeeId { get; set; }
        public Employee Employee { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();
    }
}
