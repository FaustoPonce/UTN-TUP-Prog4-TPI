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
    public interface IAdminService
    {
        Admin Create(CreationAdminDto creationAdminDto);

        List<AdminDto> GetAllAdmins();
        AdminDto GetById(int id);
        void Update(int id, CreationAdminDto creationAdminDto);
        void Delete(int id);
    }
}
