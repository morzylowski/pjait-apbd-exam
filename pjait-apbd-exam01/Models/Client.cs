namespace pjait_apbd_exam01.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        [Key]
        public int IdClient { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(128)]
        public string? Email { get; set; }

        [Phone]
        [MaxLength(64)]
        public string? Phone { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}