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
    public class PaymentService : IPaymentService
    {
        private readonly IRepositoryBase<Payment> _paymentRepositoryBase;
        public PaymentService(IRepositoryBase<Payment> paymentRepositoryBase)
        {
            _paymentRepositoryBase = paymentRepositoryBase;
        }

        public Payment Create(CreationPaymentDto creationPaymentDto)
        {
            var newPayment = new Payment
            {
                Amount = creationPaymentDto.Amount,
                MemberId = creationPaymentDto.MemberId
            };
            return _paymentRepositoryBase.create(newPayment);
        }

        public void Delete(int id)
        {
            var paymentToDelete = _paymentRepositoryBase.GetById(id);
            if (paymentToDelete != null)
            {
                _paymentRepositoryBase.Delete(paymentToDelete);
            }
        }

        public List<PaymentDto> GetAllPayments()
        {
            var payments = _paymentRepositoryBase.GetAll();
            var paymentDtos = PaymentDto.FromEntityList(payments);
            return paymentDtos;
        }

        public PaymentDto GetById(int id)
        {
            var payment = _paymentRepositoryBase.GetById(id);
            if (payment == null)
            {
                return null;
            }
            return PaymentDto.FromEntity(payment);
        }

        public void Update(int id, CreationPaymentDto creationPaymentDto)
        {
            var paymentToUpdate = _paymentRepositoryBase.GetById(id);
            if (paymentToUpdate != null)
            {
                paymentToUpdate.Amount = creationPaymentDto.Amount;
                paymentToUpdate.Date = DateTime.Now;
                paymentToUpdate.PaymentMethod = creationPaymentDto.PaymentMethod;
                paymentToUpdate.MemberId = creationPaymentDto.MemberId;
                _paymentRepositoryBase.Update(paymentToUpdate);
            }
        }
    }
}
