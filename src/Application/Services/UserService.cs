using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDto> GetAllUsers()
        {
            List<User> users = _userRepository.GetAll();
            if (users == null || users.Count == 0)
            {
                throw new NotFoundException("No hay usuarios todavia");
            }
            List<UserDto> userDtos = UserDto.FromEntityList(users);
            return userDtos;
        }

        public User Create(CreationUserDto creationUserDto) 
        {
            if (string.IsNullOrWhiteSpace(creationUserDto.Name) ||
                string.IsNullOrWhiteSpace(creationUserDto.Email) ||
                string.IsNullOrWhiteSpace(creationUserDto.Password))
            {
                throw new ValidationException("Falta un campo. Todos los campos son obligatorios.");
            }
            if (!creationUserDto.Email.Contains("@"))
            {
                throw new ValidationException("El email no es valido. Debe contener un '@'.");
            }
            var newUser = new User
            {
                Name = creationUserDto.Name,
                Email = creationUserDto.Email,
                Password = creationUserDto.Password,
            };

            return _userRepository.create(newUser);
        }

        public UserDto GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new NotFoundException($"No hay un usuario con id {id}");
            }
            var userDto = UserDto.FromEntity(user);
            return userDto;
        }

        public void Update(int id, CreationUserDto creationUserDto)
        {
            if (string.IsNullOrWhiteSpace(creationUserDto.Name) ||
                string.IsNullOrWhiteSpace(creationUserDto.Email) ||
                string.IsNullOrWhiteSpace(creationUserDto.Password))
            {
                throw new ValidationException("Falta un campo. Todos los campos son obligatorios.");
            }
            if (!creationUserDto.Email.Contains("@"))
            {
                throw new ValidationException("El email no es valido. Debe contener un '@'.");
            }
            var userToUpdate = _userRepository.GetById(id);
            if (userToUpdate == null) 
            {
                throw new NotFoundException($"No hay un usuario con id {id}");
            }
            else
            {
                userToUpdate.Name = creationUserDto.Name;
                userToUpdate.Email = creationUserDto.Email;
                userToUpdate.Password = creationUserDto.Password;
                userToUpdate.LastUpdate = DateTime.Now;
                _userRepository.Update(userToUpdate);
            }
        }

        public void Delete(int id)
        {
            var userToDelete = _userRepository.GetById(id);
            if (userToDelete == null)
            {
                throw new NotFoundException($"No hay un usuario con id {id}");
            }
            _userRepository.Delete(userToDelete);
        }
    }
}
