using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        T create(T NewEntity);
        T? GetById(int id);
        List<T> GetAll();
        void Update(T Entity);
        void Delete(T Entity);
    }
}
