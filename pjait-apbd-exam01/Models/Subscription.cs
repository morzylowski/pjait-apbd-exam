namespace pjait_apbd_exam01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Subscription
    {
        [Key]
        public int IdSubscription { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1, 12)]
        public int RenewalPeriod { get; set; }

        public DateTime EndTime { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<Discount>? Discounts { get; set; } = new List<Discount>();
    }
}