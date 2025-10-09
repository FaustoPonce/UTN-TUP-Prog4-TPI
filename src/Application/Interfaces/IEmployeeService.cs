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
    public interface IEmployeeService
    {
        Employee Create(CreationEmployeeDto creationEmployeeDto);

        List<EmployeeDto> GetAllEmployees();
        EmployeeDto GetById(int id);
        void Update(int id, CreationEmployeeDto creationEmployeeDto);
        void Delete(int id);
    }
}
