using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using Repository.Repo;
using Repository.IRepo;

namespace WebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = _repo.GetProducts();
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _repo.GetProductById((int)id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
       // [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(product);
                try
                {
                    _repo.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(product);
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _repo.GetProductById((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //product.Id = 111;
                    _repo.Edit(product);
                    _repo.SaveChanges();
                }
                catch
                {
                    ViewBag.Error = true;
                    return View(product);
                }
                
            }
            ViewBag.Error = false;
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id, bool? error)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _repo.GetProductById((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (error != null)
            {
                ViewBag.Error = true;
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteProduct(id);
            try
            {
                _repo.SaveChanges();
            }
            catch (Exception e)
            {
                return RedirectToAction("Delete", new { id = id, error = true });
            }
            return RedirectToAction("Index");
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
