namespace pjait_apbd_exam01.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Discount
    {
        [Key]
        public int IdDiscount { get; set; }

        [Range(0, 100)]
        public decimal Value { get; set; }

        public int IdSubscription { get; set; }
        public Subscription? Subscription { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}