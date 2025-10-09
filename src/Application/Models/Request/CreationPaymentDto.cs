using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreationPaymentDto
    {
        public decimal Amount { get; set; }
        public int MemberId { get; set; }
        // enum que define el metodo de pago
        public PaymentMethod PaymentMethod { get; set; }
    }
}
