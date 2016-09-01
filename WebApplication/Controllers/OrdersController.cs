using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using Microsoft.AspNet.Identity;
using Repository.IRepo;

namespace WebApplication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repo;

        public OrdersController(IOrderRepository repo)
        {
            _repo = repo;
        }

        // GET: Orders
        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            IQueryable<Order> orders = Enumerable.Empty<Order>().AsQueryable(); ;
            if ((User.IsInRole("Client")))
            {
                orders = _repo.GetOrders(userId);
            }
            else if ((User.IsInRole("Admin")))
            {
                orders = _repo.GetAllOrders();
            }
            orders = orders.OrderByDescending(o => o.OrderDate);
            return View(orders);
        }

        // GET: Orders/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null || (!User.Identity.IsAuthenticated))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var items = _repo.GetItems(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
