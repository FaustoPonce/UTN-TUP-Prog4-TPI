using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public decimal Salary { get; set; }
        public EmployeeRole Role { get; set; }

        public static EmployeeDto FromEntity(Employee employee)
        {
            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                CreatedDate = employee.CreatedDate,
                LastUpdate = employee.LastUpdate,
                Salary = employee.Salary,
                Role = employee.Role

            };
            return employeeDto;
        }

        public static List<EmployeeDto> FromEntityList(List<Employee> employeeList)
        {
            var employees = new List<EmployeeDto>();
            foreach (var employee in employeeList)
            {
                employees.Add(FromEntity(employee));
            }
            return employees;
        }
        
    }
}
