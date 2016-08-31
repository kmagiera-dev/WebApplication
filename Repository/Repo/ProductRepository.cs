using Repository.IRepo;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Repository.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IApplicationContext _db;

        public ProductRepository(IApplicationContext db)
        {
            _db = db;
        }

        public IQueryable<Product> GetProducts()
        {
            return _db.Products.AsNoTracking();
        }

        public Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }

        public void Add(Product product)
        {
            _db.Products.Add(product);
        }

        public void Edit(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(int id)
        {
            Product product = _db.Products.Find(id);
            _db.Products.Remove(product);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public IQueryable GetPage(int? page = 1, int? pageSize = 10)
        {
            var products = _db.Products
                .OrderBy(p => p.Name)
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
            return products;
        }
    }
}