using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int MemberId { get; set; }
        // enum que define el metodo de pago
        public PaymentMethod PaymentMethod { get; set; }
        public Member Member { get; set; }
    }
}
