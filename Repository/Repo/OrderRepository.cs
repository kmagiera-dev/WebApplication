using Repository.IRepo;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Repo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IApplicationContext _db;

        public OrderRepository(IApplicationContext db)
        {
            _db = db;
        }

        public IQueryable<Order> GetOrders(string userId)
        {
            return _db.Orders.AsNoTracking().Where(o => o.UserId == userId);
        }

        public IQueryable<OrderItem> GetItems(string orderId)
        {
            return _db.OrderItems.AsNoTracking().Where(i => i.OrderId == orderId);
        }
    }
}