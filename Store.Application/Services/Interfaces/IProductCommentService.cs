using Store.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Interfaces
{
    public interface IProductCommentService
    {
        void AddComment(int ProductId, int UserId, string Comment);
        ProductCommentViewModel GetCommentsForProduct(int productId);
    }
}
