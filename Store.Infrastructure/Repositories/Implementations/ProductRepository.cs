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

        public List<ProductGroup> GetProductGroups()
        {
            return _context.ProductGroups.ToList();
        }
    }
}
