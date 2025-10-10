using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PaymentDto
    {   public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } 
        public int MemberId { get; set; }
        // enum que define el metodo de pago
        public string PaymentMethod { get; set; }
        public MemberDto Member { get; set; }

        public static PaymentDto FromEntity(Payment payment)
        {
            if (payment == null) return null;
            var paymentDto = new PaymentDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Date = payment.Date,
                MemberId = payment.MemberId,
                PaymentMethod = payment.PaymentMethod.ToString(),
                Member = MemberDto.FromEntity(payment.Member)

            };
            return paymentDto;
        }

        public static List<PaymentDto> FromEntityList(List<Payment> paymentList)
        {
            var payments = new List<PaymentDto>();
            foreach (var payment in paymentList)
            {
                payments.Add(FromEntity(payment));
            }
            return payments;
        }
    }
}
