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
    public interface IPaymentService
    {
        Payment Create(CreationPaymentDto creationPaymentDto);

        List<PaymentDto> GetAllPayments();
        PaymentDto GetById(int id);
        void Update(int id, CreationPaymentDto creationPaymentDto);
        void Delete(int id);
    }
}
