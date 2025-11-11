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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public Admin Create(CreationAdminDto creationAdminDto)
        {   
            if (string.IsNullOrWhiteSpace(creationAdminDto.Name) || 
                string.IsNullOrWhiteSpace(creationAdminDto.Email) || 
                string.IsNullOrWhiteSpace(creationAdminDto.Password))
            {
                throw new ValidationException("Falta un Campo, todos son obligatorios");
            }
            if (!creationAdminDto.Email.Contains("@"))
            {
                throw new ValidationException("El email no es valido. Debe contener un '@'.");
            }
            var newAdmin = new Admin
            {
                Name = creationAdminDto.Name,
                Email = creationAdminDto.Email,
                Password = creationAdminDto.Password
               
            };
            return _adminRepository.create(newAdmin);
        }

        public void Delete(int id)
        {
            
            var adminToDelete = _adminRepository.GetById(id);
            if (adminToDelete == null)
            {
                throw new NotFoundException($"No existe un admin con id {id}");
            }
            _adminRepository.Delete(adminToDelete);
        }

        public List<AdminDto> GetAllAdmins()
        {
            
            var admins = _adminRepository.GetAll();
            if (admins == null || admins.Count == 0)
            {
                throw new NotFoundException("No hay admins");
            }
            var adminDtos = AdminDto.FromEntityList(admins);
            return adminDtos;
        }

        public AdminDto GetById(int id)
        {
            var admin = _adminRepository.GetById(id);
            if (admin == null)
            {
                throw new NotFoundException($"No existe un admin con id {id}");
            }
            return AdminDto.FromEntity(admin);
        }

        public void Update(int id, CreationAdminDto creationAdminDto)
        {
            if (string.IsNullOrWhiteSpace(creationAdminDto.Name) ||
                string.IsNullOrWhiteSpace(creationAdminDto.Email) ||
                string.IsNullOrWhiteSpace(creationAdminDto.Password))
            {
                throw new ValidationException("Falta un Campo, todos son obligatorios");
            }
            var adminToUpdate = _adminRepository.GetById(id);
            if (adminToUpdate == null)
            {
                throw new NotFoundException($"No existe un admin con id {id}");
            }
            adminToUpdate.Name = creationAdminDto.Name;
            adminToUpdate.Email = creationAdminDto.Email;
            adminToUpdate.Password = creationAdminDto.Password;
            adminToUpdate.LastUpdate = DateTime.Now;
            _adminRepository.Update(adminToUpdate);
        }
    }
}
