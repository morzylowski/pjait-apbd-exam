namespace pjait_apbd_exam01.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Payment
    {
        [Key]
        public int IdPayment { get; set; }

        public DateTime Date { get; set; }

        public int IdSale { get; set; }
        public Sale? Sale { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}