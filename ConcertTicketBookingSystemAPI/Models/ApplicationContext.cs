using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketBookingSystemAPI.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GoogleUser> GoogleUsers { get; set; }
        public DbSet<FacebookUser> FacebookUsers { get; set; }
        public DbSet<MicrosoftUser> MicrosoftUsers { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<ClassicConcert> ClassicConcerts { get; set; }
        public DbSet<OpenAirConcert> OpenAirConcerts { get; set; }
        public DbSet<PartyConcert> PartyConcerts { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromoCode>().HasIndex(promo => promo.Code).IsUnique();
            modelBuilder.Entity<Action>().Property(action => action.Date).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<Concert>().Property(concert => concert.CreationTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<GoogleUser>().HasIndex(user => user.GoogleId).IsUnique();
            modelBuilder.Entity<FacebookUser>().HasIndex(user => user.FacebookId).IsUnique();
            modelBuilder.Entity<MicrosoftUser>().HasIndex(user => user.MicrosoftId).IsUnique();
            modelBuilder.Entity<Image>().HasIndex(image => image.ConcertId);
            modelBuilder.Entity<Concert>().Property(concert => concert.Cost).HasColumnType("money");
            //modelBuilder.Entity<Concert>().HasMany(c => c.Tickets).WithOne(t => t.Concert).HasForeignKey(t => t.ConcertId);
        }
    }
}
