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
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryBase<Employee> _employeeRepositoryBase;
        public EmployeeService(IRepositoryBase<Employee> employeeRepositoryBase)
        {
            _employeeRepositoryBase = employeeRepositoryBase;
        }
        public Employee Create(CreationEmployeeDto creationEmployeeDto)
        {
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
            if (employeeToDelete != null)
            {
                _employeeRepositoryBase.Delete(employeeToDelete);
            }
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            List<Employee> employees = _employeeRepositoryBase.GetAll();
            List<EmployeeDto> employeeDtos = EmployeeDto.FromEntityList(employees);
            return employeeDtos;
        }

        public EmployeeDto GetById(int id)
        {
            var employee = _employeeRepositoryBase.GetById(id);
            if (employee == null)
            {
                return null;
            }
            var employeeDto = EmployeeDto.FromEntity(employee);
            return employeeDto;
        }

        public void Update(int id, CreationEmployeeDto creationEmployeeDto)
        {
            var employeeToUpdate = _employeeRepositoryBase.GetById(id);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.Name = creationEmployeeDto.Name;
                employeeToUpdate.Email = creationEmployeeDto.Email;
                employeeToUpdate.Password = creationEmployeeDto.Password;
                employeeToUpdate.Salary = creationEmployeeDto.Salary;
                employeeToUpdate.Role = creationEmployeeDto.Role;
                _employeeRepositoryBase.Update(employeeToUpdate);
            }
        }
    }
}
