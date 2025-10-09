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
    public interface IMemberService
    {
        Member Create(CreationMemberDto creationMemberDto);

        List<MemberDto> GetAllMembers();
        MemberDto GetById(int id);
        void Update(int id, CreationMemberDto creationMemberDto);
        void Delete(int id);
    }
}
