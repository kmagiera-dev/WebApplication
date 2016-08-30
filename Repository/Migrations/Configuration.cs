namespace Repository.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
            SeedOrders(context);
            SeedProducts(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var user = new User { UserName = "Admin" };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }
        }

        private void SeedOrders(ApplicationDbContext context)
        {
            var userId = context.Set<User>()
                    .Where(u => u.UserName == "Admin")
                    .FirstOrDefault().Id;
            for (int i = 1; i <= 10; i++)
            {
                var order = new Order()
                {
                    Id = i,
                    UserId = userId,
                    OrderDate = DateTime.Now.AddDays(-i)
                };
                context.Set<Order>().AddOrUpdate(order);
            }
            context.SaveChanges();
        }

        private void SeedProducts(ApplicationDbContext context)
        {
            for (int i = 1; i <= 10; i++)
            {
                var product = new Product()
                {
                    Id = i,
                    Name = "Product " + i.ToString(),
                    Description = "Description " + i.ToString(),
                };
                context.Set<Product>().AddOrUpdate(product);
            }
            context.SaveChanges();
        }
    }
}
