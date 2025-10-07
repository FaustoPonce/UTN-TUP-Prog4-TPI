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
    public interface IUserService
    {
        User Create(CreationUserDto creationUserDto);

        List<UserDto> GetAllUsers();
        UserDto GetById(int id);
        void Update(int id, CreationUserDto creationUserDto);
        void Delete(int id);
       // List<User> GetbyName(string nombre);

    }
}
