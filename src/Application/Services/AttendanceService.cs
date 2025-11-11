using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
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
        public AttendanceService(IRepositoryBase<Attendance> attendanceRepositoryBase, IRepositoryBase<Employee> employeeRepositoryBase, IAttendanceRepository attendanceRepository, IRepositoryBase<Member> memberRepository)
        {
            _attendanceRepositoryBase = attendanceRepositoryBase;
            _employeeRepositoryBase = employeeRepositoryBase;
            _attendanceRepository = attendanceRepository;
            _memberRepositoryBase = memberRepository;

        }

        public Attendance Create(CreationAttendanceDto creationAttendaceDto)
        {
            if (creationAttendaceDto.Date == default(DateTime))
            {
                throw new ValidationException("Falta el campo 'date' o su valor no es valido.");
            }
            // se tienen que poner al menos memberId o employeeId; no permitimos los dos a la vez
            var hayMember = creationAttendaceDto.MemberId != null && creationAttendaceDto.MemberId > 0;
            var hayEmployee = creationAttendaceDto.EmployeeId != null && creationAttendaceDto.EmployeeId > 0;

            if (!hayMember && !hayEmployee)
            {
                throw new ValidationException("Debe poner memberId o employeeId (uno de los dos).");
            }

            if (hayMember && hayEmployee)
            {
                throw new ValidationException("Solo debe poner memberId o employeeId, no ambos.");
            }
            var newAttendance = new Attendance();
            
            if (creationAttendaceDto.MemberId == 0)
            {
                var employee = _employeeRepositoryBase.GetById(creationAttendaceDto.EmployeeId.Value);
                if (employee == null)
                {
                    throw new NotFoundException($"No se encontro un empleado con id {creationAttendaceDto.EmployeeId}");
                }
                newAttendance.MemberId = null;
                newAttendance.EmployeeId = creationAttendaceDto.EmployeeId;
                newAttendance.Date = creationAttendaceDto.Date; 
                newAttendance.Employee = employee;
            }
            if (creationAttendaceDto.EmployeeId == 0)
            {
                var member = _memberRepositoryBase.GetById(creationAttendaceDto.MemberId.Value);
                if (member == null)
                {
                    throw new NotFoundException($"No se encontro un miembro con id {creationAttendaceDto.MemberId}");
                }
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
            if (attendanceToDelete == null)
            {
                throw new NotFoundException($"No existe una asistencia con id {id}");
            }
            _attendanceRepositoryBase.Delete(attendanceToDelete);
        }

        public List<AttendanceDto> GetAllAttendaces()
        {
            var attendances = _attendanceRepository.GetAll();
            if (attendances == null || attendances.Count == 0)
            {
                throw new NotFoundException("No se hay asistencias registradas.");
            }
            var attendanceDtos = AttendanceDto.FromEntityList(attendances);
            return attendanceDtos;
        }

        public AttendanceDto GetById(int id)
        {
            var attendance = _attendanceRepository.GetById(id);
            if (attendance == null)
            {
                throw new NotFoundException($"No existe una asistencia con id {id}");
            }
            return AttendanceDto.FromEntity(attendance);


        }

        public void Update(int id, CreationAttendanceDto creationAttendaceDto)
        {
           
            if (creationAttendaceDto.Date == default(DateTime))
            {
                throw new ValidationException("Falta el campo 'date' o su valor no es valido.");
            }
            // (igual que Create)
            int? memberId = (creationAttendaceDto.MemberId.HasValue && creationAttendaceDto.MemberId.Value > 0) ? creationAttendaceDto.MemberId.Value : null;
            int? employeeId = (creationAttendaceDto.EmployeeId.HasValue && creationAttendaceDto.EmployeeId.Value > 0) ? creationAttendaceDto.EmployeeId.Value : null;

            var hayMember = memberId != null;
            var hayEmployee = employeeId != null;
            if (!hayMember && !hayEmployee)
            {
                throw new ValidationException("Debe proporcionar memberId o employeeId (uno de los dos).");
            }

            if (hayMember && hayEmployee)
            {
                throw new ValidationException("Solo debe proporcionar memberId o employeeId, no ambos.");
            }
            var attendanceToUpdate = _attendanceRepositoryBase.GetById(id);
            if (attendanceToUpdate == null)
            {
                throw new NotFoundException($"No existe una asistencia con id {id}");
            }
            if (hayMember)
            {
                var member = _memberRepositoryBase.GetById(memberId.Value);
                if (member == null)
                {
                    throw new NotFoundException($"No se encontro un miembro con id {memberId}");
                }

                attendanceToUpdate.MemberId = memberId;
                attendanceToUpdate.Member = member;
                attendanceToUpdate.EmployeeId = null;
                attendanceToUpdate.Employee = null;
            }
            else // hayEmployee
            {
                var employee = _employeeRepositoryBase.GetById(employeeId.Value);
                if (employee == null)
                {
                    throw new NotFoundException($"No se encontro un empleado con id {employeeId}");
                }

                attendanceToUpdate.EmployeeId = employeeId;
                attendanceToUpdate.Employee = employee;
                attendanceToUpdate.MemberId = null;
                attendanceToUpdate.Member = null;
            }
            attendanceToUpdate.Date = creationAttendaceDto.Date;
            _attendanceRepositoryBase.Update(attendanceToUpdate);
        }
    }
}
