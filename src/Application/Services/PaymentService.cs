using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRepositoryBase<Member> _memberRepositoryBase;
        public PaymentService(IRepositoryBase<Payment> paymentRepositoryBase, IPaymentRepository paymentRepository, IRepositoryBase<Member> memberRepositoryBase)
        {
            _paymentRepositoryBase = paymentRepositoryBase;
            _paymentRepository = paymentRepository;
            _memberRepositoryBase = memberRepositoryBase;
        }

        public Payment Create(CreationPaymentDto creationPaymentDto)
        {
            if (creationPaymentDto.Amount <= 0)
            {
                throw new ValidationException("El monto del pago debe ser mayor a 0.");
            }

            if (creationPaymentDto.MemberId <= 0)
            {
                throw new ValidationException("Falta el campo 'memberId' o su valor no es valido.");
            }

            if (!Enum.IsDefined(typeof(PaymentMethod), creationPaymentDto.PaymentMethod))
            {
                throw new ValidationException("El campo 'paymentMethod' no es valido. Debe ser 0 (Efectivo) o 1 (Tarjeta).");
            }
            if (_memberRepositoryBase.GetById(creationPaymentDto.MemberId) == null)
            {
                throw new NotFoundException($"No se encontro un miembro con id {creationPaymentDto.MemberId} para asociar el pago.");
            }
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
            if (paymentToDelete == null)
            {
                throw new NotFoundException($"No existe un pago con id {id}");
            }
            _paymentRepositoryBase.Delete(paymentToDelete);
        }

        public List<PaymentDto> GetAllPayments()
        {
            var payments = _paymentRepository.GetAll();
            if (payments == null || payments.Count == 0)
            {
                throw new NotFoundException("No existen pagos todavia");
            }
            var paymentDtos = PaymentDto.FromEntityList(payments);
            return paymentDtos;
        }

        public PaymentDto GetById(int id)
        {
            var payment = _paymentRepository.GetById(id);
            if (payment == null)
            {
                throw new NotFoundException($"No existe un pago con id {id}");
            }
            return PaymentDto.FromEntity(payment);
        }

        public void Update(int id, CreationPaymentDto creationPaymentDto)
        {
            if (creationPaymentDto.Amount <= 0)
            {
                throw new ValidationException("El monto del pago debe ser mayor a 0.");
            }

            if (creationPaymentDto.MemberId <= 0)
            {
                throw new ValidationException("Falta el campo 'memberId' o su valor no es valido.");
            }

            if (!Enum.IsDefined(typeof(PaymentMethod), creationPaymentDto.PaymentMethod))
            {
                throw new ValidationException("El campo 'paymentMethod' no es valido. Debe ser 0 (Efectivo) o 1 (Tarjeta).");
            }
            var paymentToUpdate = _paymentRepositoryBase.GetById(id);
            if (paymentToUpdate == null)
            {
                throw new NotFoundException($"No existe un pago con id {id}");
            }
            paymentToUpdate.Amount = creationPaymentDto.Amount;
            paymentToUpdate.Date = DateTime.Now;
            paymentToUpdate.PaymentMethod = creationPaymentDto.PaymentMethod;
            paymentToUpdate.MemberId = creationPaymentDto.MemberId;
            _paymentRepositoryBase.Update(paymentToUpdate);
        }
    }
}
