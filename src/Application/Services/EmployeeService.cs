using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryBase<Employee> _employeeRepositoryBase;
        public EmployeeService(IRepositoryBase<Employee> employeeRepositoryBase)
        {
            _employeeRepositoryBase = employeeRepositoryBase;
        }
        public Employee Create(CreationEmployeeDto creationEmployeeDto)
        {
            if (string.IsNullOrWhiteSpace(creationEmployeeDto.Name) ||
                string.IsNullOrWhiteSpace(creationEmployeeDto.Email) ||
                string.IsNullOrWhiteSpace(creationEmployeeDto.Password))
            {
                throw new ValidationException("Falta un campo. Todos los campos de texto son obligatorios.");
            }
            if (creationEmployeeDto.Salary <= 0)
            {
                throw new ValidationException("El salario debe ser mayor a 0.");
            }
            if (!Enum.IsDefined(typeof(EmployeeRole), creationEmployeeDto.Role))
            {
                throw new ValidationException("El campo 'role' no es valido. Debe ser 0 (Limpieza), 1 (Profesor) o 2 (Recepcionista).");
            }
            if (!creationEmployeeDto.Email.Contains("@"))
            {
                throw new ValidationException("El email no es valido. Debe contener un '@'.");
            }
            var newEmployee = new Employee
            {
                Name = creationEmployeeDto.Name,
                Email = creationEmployeeDto.Email,
                Password = creationEmployeeDto.Password,
                Salary = creationEmployeeDto.Salary,
                Role = creationEmployeeDto.Role
            };
            return _employeeRepositoryBase.create(newEmployee);
        }

        public void Delete(int id)
        {
            var employeeToDelete = _employeeRepositoryBase.GetById(id);
            if (employeeToDelete == null)
            {
                throw new NotFoundException($"No existe un empleado con id {id}");
            }
            _employeeRepositoryBase.Delete(employeeToDelete);
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            List<Employee> employees = _employeeRepositoryBase.GetAll();
            if (employees == null || employees.Count == 0)
            {
                throw new NotFoundException("No hay empleados");
            }
            List<EmployeeDto> employeeDtos = EmployeeDto.FromEntityList(employees);
            return employeeDtos;
        }

        public EmployeeDto GetById(int id)
        {
            var employee = _employeeRepositoryBase.GetById(id);
            if (employee == null)
            {
                throw new NotFoundException($"No existe un empleado con id {id}");
            }
            var employeeDto = EmployeeDto.FromEntity(employee);
            return employeeDto;
        }

        public void Update(int id, CreationEmployeeDto creationEmployeeDto)
        {
            if (string.IsNullOrWhiteSpace(creationEmployeeDto.Name) ||
                string.IsNullOrWhiteSpace(creationEmployeeDto.Email) ||
                string.IsNullOrWhiteSpace(creationEmployeeDto.Password))
            {
                throw new ValidationException("Falta un campo. Todos los campos de texto son obligatorios.");
            }
            if (creationEmployeeDto.Salary <= 0)
            {
                throw new ValidationException("El salario debe ser mayor a 0.");
            }
            if (!Enum.IsDefined(typeof(EmployeeRole), creationEmployeeDto.Role))
            {
                throw new ValidationException("El campo 'role' no es valido. Debe ser 0 (Limpieza), 1 (Profesor) o 2 (Recepcionista).");
            }
            if (!creationEmployeeDto.Email.Contains("@"))
            {
                throw new ValidationException("El email no es valido. Debe contener un '@'.");
            }
            var employeeToUpdate = _employeeRepositoryBase.GetById(id);
            if (employeeToUpdate == null)
            {
                throw new NotFoundException($"No existe un empleado con id {id}");
            }
            employeeToUpdate.Name = creationEmployeeDto.Name;
            employeeToUpdate.Email = creationEmployeeDto.Email;
            employeeToUpdate.Password = creationEmployeeDto.Password;
            employeeToUpdate.Salary = creationEmployeeDto.Salary;
            employeeToUpdate.Role = creationEmployeeDto.Role;
            employeeToUpdate.LastUpdate = DateTime.Now;
            _employeeRepositoryBase.Update(employeeToUpdate);
        }
    }
}
