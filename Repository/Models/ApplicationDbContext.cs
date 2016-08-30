using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repository.IRepo;

namespace Repository.Models
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Potrzebne dla klas Identity
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove< PluralizingTableNameConvention > ();
            modelBuilder.Conventions.Remove< OneToManyCascadeDeleteConvention > ();
            modelBuilder.Entity<Order>().HasRequired(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}
