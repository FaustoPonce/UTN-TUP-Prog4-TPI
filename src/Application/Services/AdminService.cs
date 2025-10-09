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
    public class AdminService : IAdminService
    {
        private readonly IRepositoryBase<Admin> _adminRepositoryBase;
        public AdminService(IRepositoryBase<Admin> adminRepository)
        {
            _adminRepositoryBase = adminRepository;
        }
        public Admin Create(CreationAdminDto creationAdminDto)
        {
            var newAdmin = new Admin
            {
                Name = creationAdminDto.Name,
                Email = creationAdminDto.Email,
                Password = creationAdminDto.Password
               
            };
            return _adminRepositoryBase.create(newAdmin);
        }

        public void Delete(int id)
        {
            var adminToDelete = _adminRepositoryBase.GetById(id);
            if (adminToDelete != null)
            {
                _adminRepositoryBase.Delete(adminToDelete);
            }
        }

        public List<AdminDto> GetAllAdmins()
        {
            var admins = _adminRepositoryBase.GetAll();
            var adminDtos = AdminDto.FromEntityList(admins);
            return adminDtos;
        }

        public AdminDto GetById(int id)
        {
            var admin = _adminRepositoryBase.GetById(id);
            return AdminDto.FromEntity(admin);
        }

        public void Update(int id, CreationAdminDto creationAdminDto)
        {
            var adminToUpdate = _adminRepositoryBase.GetById(id);
            if (adminToUpdate != null)
            {
                adminToUpdate.Name = creationAdminDto.Name;
                adminToUpdate.Email = creationAdminDto.Email;
                adminToUpdate.Password = creationAdminDto.Password;
                adminToUpdate.LastUpdate = DateTime.Now;
                _adminRepositoryBase.Update(adminToUpdate);
            }
        }
    }
}
