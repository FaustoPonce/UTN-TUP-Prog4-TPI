using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IRepositoryBase<Attendance> _attendanceRepositoryBase;
        public AttendanceService(IRepositoryBase<Attendance> attendanceRepositoryBase)
        {
            _attendanceRepositoryBase = attendanceRepositoryBase;
        }

        public Attendance Create(CreationAttendanceDto creationAttendaceDto)
        {
            var newAttendace = new Attendance
            {
                UserId = creationAttendaceDto.UserId,
                Date = creationAttendaceDto.Date,
                EmployeeId = creationAttendaceDto.EmployeeId
            };
            return _attendanceRepositoryBase.create(newAttendace);

        }

        public void Delete(int id)
        {
            var attendanceToDelete = _attendanceRepositoryBase.GetById(id);
            if (attendanceToDelete != null)
            {
                _attendanceRepositoryBase.Delete(attendanceToDelete);
            }
        }

        public List<AttendanceDto> GetAllAttendaces()
        {
            var attendances = _attendanceRepositoryBase.GetAll();
            var attendanceDtos = AttendanceDto.FromEntityList(attendances);
            return attendanceDtos;
        }

        public AttendanceDto GetById(int id)
        {
            var attendance = _attendanceRepositoryBase.GetById(id);
            return AttendanceDto.FromEntity(attendance);

        }

        public void Update(int id, CreationAttendanceDto creationAttendaceDto)
        {
            var attendanceToUpdate = _attendanceRepositoryBase.GetById(id);
            if (attendanceToUpdate != null)
            {
                attendanceToUpdate.UserId = creationAttendaceDto.UserId;
                attendanceToUpdate.Date = creationAttendaceDto.Date;
                attendanceToUpdate.EmployeeId = creationAttendaceDto.EmployeeId;
                _attendanceRepositoryBase.Update(attendanceToUpdate);
            }
        }
    }
}
