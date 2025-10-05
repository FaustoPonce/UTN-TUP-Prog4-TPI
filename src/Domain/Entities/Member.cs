using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Member : User
    {
        public DateTime CreationDate { get; set; }
        public enum State
        {
            Activo,
            Inactivo,
            EnDeuda
        }
        public List<WorkoutClass> WorkoutClasses { get; set; } = new List<WorkoutClass>();
    }
}
