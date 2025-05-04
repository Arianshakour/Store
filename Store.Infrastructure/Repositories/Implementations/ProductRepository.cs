using Microsoft.AspNetCore.Mvc;
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

        public List<Product> GetLastProducts()
        {
            return _context.Products.OrderByDescending(x=>x.ProductId).
                Include(x => x.productGroup).Include(x => x.subProductGroup).Take(8).ToList();
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

        public List<Product> ShowAllProduct(string search, string type, string orderby,
            int startPrice, int endPrice, List<int> selectedGroups)
        {
            var data = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.ProductTitle.Contains(search));
            }
            switch (type)
            {
                case "all":
                    break;
                case "buy":
                    {
                        data = data.Where(x => x.Price != 0);
                        break;
                    }
                case "free":
                    {
                        data = data.Where(x => x.Price == 0);
                        break;
                    }
            }
            switch (orderby)
            {
                case "all":
                    break;
                case "date":
                    {
                        data = data.OrderByDescending(x => x.CreateDate);
                        break;
                    }
                case "price":
                    {
                        data = data.OrderByDescending(x => x.Price);
                        break;
                    }
            }
            if (startPrice > 0)
            {
                data = data.Where(c => c.Price > startPrice);
            }

            if (endPrice > 0)
            {
                data = data.Where(c => c.Price < startPrice);
            }
            if(selectedGroups !=null && selectedGroups.Any())
            {
                foreach(var groupid in selectedGroups)
                {
                    data = data.Where(x => x.GroupId == groupid || x.SubGroup == groupid);
                }
            }
            return data.ToList();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
