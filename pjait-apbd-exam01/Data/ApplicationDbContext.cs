// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using pjait_apbd_exam01.Models;

namespace pjait_apbd_exam01.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Discount> Discounts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(c => c.IdClient);
            modelBuilder.Entity<Subscription>()
                .HasKey(s => s.IdSubscription);
            modelBuilder.Entity<Sale>()
                .HasKey(s => s.IdSale);
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.IdPayment);
            modelBuilder.Entity<Discount>()
                .HasKey(d => d.IdDiscount);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Client)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.IdClient);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Subscription)
                .WithMany(sub => sub.Sales)
                .HasForeignKey(s => s.IdSubscription);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Sale)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.IdSale);

            modelBuilder.Entity<Discount>()
                .HasOne(d => d.Subscription)
                .WithMany(sub => sub.Discounts)
                .HasForeignKey(d => d.IdSubscription);
        }
    }
}