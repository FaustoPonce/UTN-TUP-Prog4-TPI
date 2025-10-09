using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes
{
    public class Schedule
    {
        public int ClassDays { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
