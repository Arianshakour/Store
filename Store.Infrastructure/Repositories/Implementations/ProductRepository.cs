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

        public void AddProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Add(productGroup);
        }

        public void DeleteProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Remove(productGroup);
        }

        public List<Product> GetLastProducts()
        {
            return _context.Products.OrderByDescending(x=>x.ProductId).
                Include(x => x.productGroup).Include(x => x.subProductGroup).Take(8).ToList();
        }
        public List<Product> GetPopularProducts()
        {
            return _context.Products.OrderByDescending(x => x.ProductId).
                Include(x => x.productGroup).Include(x => x.subProductGroup)
                .Include(x=>x.orderDetails).OrderByDescending(x=>x.orderDetails.Count()).Take(8).ToList();
        }
        public Product GetProductById(int id)
        {
            var p = _context.Products.Include(x => x.productGroup).Include(x=>x.subProductGroup)
                .Include(x => x.user).Include(x=>x.productComments).FirstOrDefault(x => x.ProductId == id);
            if (p == null)
            {
                throw new NullReferenceException();
            }
            return p;
        }

        public ProductGroup GetProductGroupById(int id)
        {
            var p = _context.ProductGroups.Include(x => x.productGroups).FirstOrDefault(x=>x.GroupId==id);
            if (p == null)
            {
                throw new NullReferenceException();
            }
            return p;
        }

        public List<ProductGroup> GetProductGroups()
        {
            return _context.ProductGroups.Include(x=>x.productGroups).ToList();
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
            var data = _context.Products.Include(x=>x.productGroup).Include(x=>x.subProductGroup).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.ProductTitle.Contains(search) || x.Tags.Contains(search));
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
                data = data.Where(c => c.Price < endPrice);
            }
            if(selectedGroups !=null && selectedGroups.Any())
            {
                //foreach(var groupid in selectedGroups)
                //{
                //    data = data.Where(x => x.GroupId == groupid || x.SubGroup == groupid);
                //}
                data = data.Where(x => selectedGroups.Contains(x.GroupId) || selectedGroups.Contains((int)x.SubGroup));
            }
            return data.ToList();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public void UpdateProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Add(productGroup);
        }
    }
}
