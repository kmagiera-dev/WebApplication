using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepo
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrders(string userId);
        IQueryable<Order> GetAllOrders();
        IQueryable<OrderItem> GetItems(string orderId);
    }
}
