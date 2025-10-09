using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAttendanceService
    {
        Attendance Create(CreationAttendanceDto creationAttendaceDto);

        List<AttendanceDto> GetAllAttendaces();
        AttendanceDto GetById(int id);
        void Update(int id, CreationAttendanceDto creationAttendaceDto);
        void Delete(int id);
    }
}
