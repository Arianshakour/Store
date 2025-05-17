using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using Store.Domain.ViewModels;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Implementations
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly IProductCommentRepository _productCommentRepository;
        public ProductCommentService(IProductCommentRepository productCommentRepository)
        {
            _productCommentRepository = productCommentRepository;
        }

        public void AddComment(int ProductId, int UserId, string Comment)
        {
            var c = new ProductComment()
            {
                ProductId = ProductId,
                UserId = UserId,
                Comment = Comment,
                CreateDate = DateTime.Now,
                IsShow = false
            };
            _productCommentRepository.Insert(c);
            _productCommentRepository.Save();
        }

        public ProductCommentViewModel GetCommentsForProduct(int productId)
        {
            var data = _productCommentRepository.GetCommentsForProduct(productId);
            return new ProductCommentViewModel()
            {
                productCommentList = data
            };
        }
    }
}
