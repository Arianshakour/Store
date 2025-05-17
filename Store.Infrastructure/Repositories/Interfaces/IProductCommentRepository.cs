using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IProductCommentRepository
    {
        List<ProductComment> GetComments();
        List<ProductComment> GetCommentsForProduct(int productId);
        ProductComment GetComment(int id);
        void Insert(ProductComment comment);
        void Update(ProductComment comment);
        void Delete(ProductComment comment);
        void Save();
    }
}
