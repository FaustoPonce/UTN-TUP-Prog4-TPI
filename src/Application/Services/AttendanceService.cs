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
        public AttendanceService(IRepositoryBase<Attendance> attendanceRepositoryBase, IRepositoryBase<Employee> employeeRepositoryBase, IAttendanceRepository attendanceRepository)
        {
            _attendanceRepositoryBase = attendanceRepositoryBase;
            _employeeRepositoryBase = employeeRepositoryBase;
            _attendanceRepository = attendanceRepository;
        }

        public Attendance Create(CreationAttendanceDto creationAttendaceDto)
        {
            if (creationAttendaceDto.Date == default(DateTime))
            {
                throw new ValidationException("Falta el campo 'date' o su valor no es valido.");
            }
            if (creationAttendaceDto.MemberId <= 0)
            {
                throw new ValidationException("Falta el campo 'memberId' o su valor no es valido.");

            }
            if (creationAttendaceDto.EmployeeId <= 0)
            {
                throw new ValidationException("Falta el campo 'employeeId' o su valor no es valido.");
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
            if (creationAttendaceDto.MemberId <= 0)
            {
                throw new ValidationException("Falta el campo 'memberId' o su valor no es valido.");

            }
            if (creationAttendaceDto.EmployeeId <= 0)
            {
                throw new ValidationException("Falta el campo 'employeeId' o su valor no es valido.");
            }
            var attendanceToUpdate = _attendanceRepositoryBase.GetById(id);
            if (attendanceToUpdate == null)
            {
                throw new NotFoundException($"No existe una asistencia con id {id}");
            }
            if (creationAttendaceDto.MemberId != null)
            {
                if (creationAttendaceDto.MemberId <= 0)
                {
                    throw new ValidationException("El campo 'memberId' no es valido (debe ser mayor a 0).");
                }

                var member = _memberRepositoryBase.GetById(creationAttendaceDto.MemberId.Value);
                if (member == null)
                {
                    throw new NotFoundException($"No se encontro un miembro con id {creationAttendaceDto.MemberId}");
                }

                attendanceToUpdate.MemberId = creationAttendaceDto.MemberId;
                attendanceToUpdate.Member = member;
                // si la asisst es de miembro employee en nulo
                attendanceToUpdate.EmployeeId = null;
                attendanceToUpdate.Employee = null;
            }
            if (creationAttendaceDto.EmployeeId != null)
            {
                if (creationAttendaceDto.EmployeeId <= 0)
                {
                    throw new ValidationException("El campo 'employeeId' no es valido (debe ser mayor a 0).");
                }

                var employee = _employeeRepositoryBase.GetById(creationAttendaceDto.EmployeeId.Value);
                if (employee == null)
                {
                    throw new NotFoundException($"No se encontro un empleado con id {creationAttendaceDto.EmployeeId}");
                }

                attendanceToUpdate.EmployeeId = creationAttendaceDto.EmployeeId;
                attendanceToUpdate.Employee = employee;
                // lo mismo al revez
                attendanceToUpdate.MemberId = null;
                attendanceToUpdate.Member = null;
            }
            attendanceToUpdate.Date = creationAttendaceDto.Date;
            _attendanceRepositoryBase.Update(attendanceToUpdate);
        }
    }
}
