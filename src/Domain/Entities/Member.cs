using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Member : User
    {
        // enum que define el estado del miembro
        public MemberState State { get; set; }
        public List<WorkoutClass> WorkoutClasses { get; set; } = new List<WorkoutClass>();
    }
}
