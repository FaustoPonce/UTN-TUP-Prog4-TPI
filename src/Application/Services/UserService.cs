using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _userRepositoryBase;

        public UserService(IRepositoryBase<User> userRepositoryBase)
        {
            _userRepositoryBase = userRepositoryBase;
        }

        public List<UserDto> GetAllUsers()
        {
            List<User> users = _userRepositoryBase.GetAll();
            List<UserDto> userDtos = UserDto.FromEntityList(users);
            return userDtos;
        }

        public User Create(CreationUserDto creationUserDto) 
        {
            var newUser = new User
            {
                Name = creationUserDto.Name,
                Email = creationUserDto.Email,
                Password = creationUserDto.Password,
            };
            
            return _userRepositoryBase.create(newUser);
        }

        public UserDto GetById(int id)
        {
            var user = _userRepositoryBase.GetById(id);
            if (user == null)
            {
                return null;
            }
            var userDto = UserDto.FromEntity(user);
            return userDto;
        }

        public void Update(int id, CreationUserDto creationUserDto)
        {   
            Console.WriteLine($"Updating user with ID: {id}");
            var userToUpdate = _userRepositoryBase.GetById(id);
            if (userToUpdate == null) 
            {
                Console.WriteLine($"User with ID: {id} not found.");
                return;
            }
            else
            {
                userToUpdate.Name = creationUserDto.Name;
                userToUpdate.Email = creationUserDto.Email;
                userToUpdate.Password = creationUserDto.Password;
                userToUpdate.LastUpdate = DateTime.Now;
                _userRepositoryBase.Update(userToUpdate);
            }
        }

        public void Delete(int id)
        {
            var userToDelete = _userRepositoryBase.GetById(id);
            if (userToDelete != null)
            {
                _userRepositoryBase.Delete(userToDelete);
            }
        }
    }
}
