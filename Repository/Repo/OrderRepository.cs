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

        public IQueryable<Order> GetOrders()
        {
            return _db.Orders.AsNoTracking();
        }
    }
}