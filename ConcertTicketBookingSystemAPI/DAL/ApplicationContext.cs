using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<GoogleUser> GoogleUsers { get; set; }
        public virtual DbSet<FacebookUser> FacebookUsers { get; set; }
        public virtual DbSet<MicrosoftUser> MicrosoftUsers { get; set; }
        public virtual DbSet<Concert> Concerts { get; set; }
        public virtual DbSet<ClassicConcert> ClassicConcerts { get; set; }
        public virtual DbSet<OpenAirConcert> OpenAirConcerts { get; set; }
        public virtual DbSet<PartyConcert> PartyConcerts { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<AdditionalImage> AdditionalImages { get; set; }
        public virtual DbSet<PromoCode> PromoCodes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
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
            modelBuilder.Entity<AdditionalImage>().HasIndex(image => image.ConcertId);
            modelBuilder.Entity<Concert>().Property(concert => concert.Cost).HasColumnType("money");
            modelBuilder.Entity<Ticket>().HasOne(c => c.Concert).WithMany(t => t.Tickets).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>().HasOne(c => c.User).WithMany(u => u.Tickets).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Ticket>().HasOne(c => c.PromoCode).WithMany(p => p.Tickets).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AdditionalImage>().HasOne(i => i.Image).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
