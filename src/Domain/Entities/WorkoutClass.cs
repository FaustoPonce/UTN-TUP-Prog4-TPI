using Domain.Classes;
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
        // schedule es una clase con otras propiedades como dia, hora de inicio, hora de fin
        public Schedule Schedule { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();
    }
}
