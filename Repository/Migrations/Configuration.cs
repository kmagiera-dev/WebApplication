namespace Repository.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Models.ApplicationDbContext>
    {
        private const int ProductIdMinValue = 1;
        private const int ProductIdMaxValue = 10;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
            SeedProducts(context);
            SeedOrders(context);
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
            if (!roleManager.RoleExists("Client"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Client";
                roleManager.Create(role);
            }
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var user = new User { UserName = "Admin@AspNetMvc.pl" };
                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }

            for (int i = 1; i < 10; i++)
            {
                string userName = "client" + i;
                if (!context.Users.Any(u => u.UserName == userName))
                {
                    var user = new User { UserName = userName + "@AspNetMvc.pl" };
                    var adminresult = manager.Create(user, "12345678");
                    if (adminresult.Succeeded)
                    {
                        manager.AddToRole(user.Id, "Client");
                    }
                }
            }
        }

        private void SeedOrders(ApplicationDbContext context)
        {
            Random orderId = new Random((int)DateTime.Now.Ticks);
            for (int i = 1; i < 10; i++)
            {
                string userName = "client" + i + "@AspNetMvc.pl";
                var userId = context.Set<User>()
                    .Where(u => u.UserName == userName)
                    .FirstOrDefault().Id;
                for (int j = 1; j <= 10; j++)
                {
                    string generatedId = orderId.Next(1, int.MaxValue).ToString();
                    var order = new Order()
                    {
                        UserId = userId,
                        OrderId = generatedId,
                        OrderDate = DateTime.Now.AddDays(-j),
                        OrderValue = SeedOrderItems(context, generatedId).ToString("C2")
                    };
                    context.Set<Order>().AddOrUpdate(order);
                }
            }
            context.SaveChanges();
        }

        private decimal SeedOrderItems(ApplicationDbContext context, string orderId)
        {
            decimal result = 0;
            Random amount = new Random((int)DateTime.Now.Ticks);
            var products = from c in context.Set<Product>()
                select new { Id = c.Id, Price = c.Price };
            Dictionary<int, decimal> dictionary = products.ToDictionary(item => item.Id, item => item.Price);

            foreach (var d in dictionary)
            {
                var item = new OrderItem()
                {
                    ProductId = d.Key,
                    OrderId = orderId,
                    Amount = amount.Next(1, 100)
                };
                context.Set<OrderItem>().AddOrUpdate(item);
                result += (item.Amount*d.Value);
            }
            return result;
        }

        private void SeedProducts(ApplicationDbContext context)
        {
            Random price = new Random((int)DateTime.Now.Ticks);
            for (int i = ProductIdMinValue; i <= ProductIdMaxValue; i++)
            {
                var product = new Product()
                {
                    Id = i,
                    Name = "Product " + i.ToString(),
                    Description = "Description " + i.ToString(),
                    Price = Convert.ToDecimal(price.NextDouble() * 100),
                };
                context.Set<Product>().AddOrUpdate(product);
            }
            context.SaveChanges();
        }
    }
}
