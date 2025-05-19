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
    public class ProductCommentRepository : IProductCommentRepository
    {
        private readonly MyContext _context;
        public ProductCommentRepository(MyContext context)
        {
            _context = context;
        }

        public void Delete(ProductComment comment)
        {
            _context.ProductComments.Remove(comment);
        }

        public ProductComment GetComment(int id)
        {
            var c = _context.ProductComments.FirstOrDefault(x => x.CommentId == id);
            if (c == null)
            {
                throw new NullReferenceException();
            }
            return c;
        }

        public List<ProductComment> GetComments()
        {
            return _context.ProductComments.ToList();
        }

        public List<ProductComment> GetCommentsForProduct(int productId)
        {
            return _context.ProductComments.IgnoreQueryFilters().Where(x=>x.product.ProductId==productId).ToList();
        }

        public ProductComment GetCommentWithIgnore(int id)
        {
            var c = _context.ProductComments.IgnoreQueryFilters().FirstOrDefault(x => x.CommentId == id);
            if (c == null)
            {
                throw new NullReferenceException();
            }
            return c;
        }

        public void Insert(ProductComment comment)
        {
            _context.ProductComments.Add(comment);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ProductComment comment)
        {
            _context.ProductComments.Update(comment);
        }
    }
}
