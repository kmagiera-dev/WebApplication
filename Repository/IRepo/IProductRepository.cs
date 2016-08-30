using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepo
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts();
        Product GetProductById(int id);
        void Add(Product product);
        void Edit(Product product);
        void DeleteProduct(int id);
        void SaveChanges();
    }
}
