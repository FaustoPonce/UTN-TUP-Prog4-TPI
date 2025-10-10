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
        private readonly IRepositoryBase<Member> _memberRepositoryBase;
        private readonly IRepositoryBase<Employee> _employeeRepositoryBase;
        private readonly IAttendanceRepository _attendanceRepository;
        public AttendanceService(IRepositoryBase<Attendance> attendanceRepositoryBase, IRepositoryBase<Employee> employeeRepositoryBase, IAttendanceRepository attendanceRepository)
        {
            _attendanceRepositoryBase = attendanceRepositoryBase;
            _employeeRepositoryBase = employeeRepositoryBase;
            _attendanceRepository = attendanceRepository;
        }

        public Attendance Create(CreationAttendanceDto creationAttendaceDto)
        {
            var newAttendance = new Attendance();
            
            if (creationAttendaceDto.MemberId == 0)
            {
                var employee = _employeeRepositoryBase.GetById(creationAttendaceDto.EmployeeId.Value);
                newAttendance.MemberId = null;
                newAttendance.EmployeeId = creationAttendaceDto.EmployeeId;
                newAttendance.Date = creationAttendaceDto.Date; 
                newAttendance.Employee = employee;
            }
            if (creationAttendaceDto.EmployeeId == 0)
            {
                var member = _memberRepositoryBase.GetById(creationAttendaceDto.MemberId.Value);
                newAttendance.MemberId = creationAttendaceDto.MemberId;
                newAttendance.EmployeeId = null;
                newAttendance.Date = creationAttendaceDto.Date;
                newAttendance.Member = member;

            }
            return _attendanceRepositoryBase.create(newAttendance);

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
            var attendances = _attendanceRepository.GetAll();
            var attendanceDtos = AttendanceDto.FromEntityList(attendances);
            return attendanceDtos;
        }

        public AttendanceDto GetById(int id)
        {
            var attendance = _attendanceRepository.GetById(id);
            return AttendanceDto.FromEntity(attendance);


        }

        public void Update(int id, CreationAttendanceDto creationAttendaceDto)
        {
            var attendanceToUpdate = _attendanceRepositoryBase.GetById(id);
            if (attendanceToUpdate != null)
            {
                attendanceToUpdate.MemberId = creationAttendaceDto.MemberId;
                attendanceToUpdate.Date = creationAttendaceDto.Date;
                attendanceToUpdate.EmployeeId = creationAttendaceDto.EmployeeId;
                _attendanceRepositoryBase.Update(attendanceToUpdate);
            }
        }
    }
}
