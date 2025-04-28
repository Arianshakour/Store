using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Context;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _context;
        public ProductRepository(MyContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public Product GetProductById(int id)
        {
            var p = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (p == null)
            {
                throw new NullReferenceException();
            }
            return p;
        }

        public List<ProductGroup> GetProductGroups()
        {
            return _context.ProductGroups.ToList();
        }

        public List<Product> GetProducts()
        {
            return _context.Products.Include(x=>x.productGroup).Include(x=>x.subProductGroup).ToList();
        }

        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
