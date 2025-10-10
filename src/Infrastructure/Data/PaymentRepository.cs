using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Payment> GetAll()
        {
            return _context.Payments.Include(p => p.Member).ToList();
        }

        public Payment GetById(int id)
        {
            return _context.Payments.Include(p => p.Member).FirstOrDefault(p => p.Id == id);
        }
    }
}
