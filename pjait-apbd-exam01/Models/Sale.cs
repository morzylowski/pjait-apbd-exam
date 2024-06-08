namespace pjait_apbd_exam01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Sale
    {
        [Key]
        public int IdSale { get; set; }

        public int IdClient { get; set; }
        public Client? Client { get; set; }

        public int IdSubscription { get; set; }
        public Subscription? Subscription { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}