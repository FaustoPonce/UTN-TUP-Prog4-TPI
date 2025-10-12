using Domain.Classes;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreationWorkoutClassDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        // schedule es una clase con otras propiedades como dia, hora de inicio, hora de fin
        public Schedule Schedule { get; set; }
        public int EmployeeId { get; set; }
        // Lista de ids de miembros que asistirán a la clase MODIFICAR DESPUES DECIDIR COMO HACERLO
        public List<int> IdMembers { get; set; } = new List<int>();
    }
}
