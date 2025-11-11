using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public T create(T NewEntity)
        {
            _context.Set<T>().Add(NewEntity);
            _context.SaveChanges();
            return NewEntity;
        }

        public void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
            _context.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T? GetById(int id)
        {
            return _context.Set<T>().Find(new object[] { id });
        }

        public void Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            _context.SaveChanges();
        }
    }
}
