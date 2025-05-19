using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel.ProductComment;
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

        public void DeleteComment(DeleteCommentDto delete)
        {
            var data = _productCommentRepository.GetCommentWithIgnore(delete.CommentId);
            _productCommentRepository.Delete(data);
            _productCommentRepository.Save();
        }

        public void EditComment(EditCommentDto edit)
        {
            var data = _productCommentRepository.GetCommentWithIgnore(edit.CommentId);
            data.Comment = edit.Comment;
            data.IsShow = edit.IsShow;
            _productCommentRepository.Update(data);
            _productCommentRepository.Save();
        }

        public DeleteCommentDto GetCommentForDelete(int id)
        {
            var data = _productCommentRepository.GetCommentWithIgnore(id);
            return new DeleteCommentDto()
            {
                Comment = data.Comment,
                CreateDate = data.CreateDate,
                CommentId = data.CommentId,
                IsShow = data.IsShow
            };
        }

        public DetailsCommentDto GetCommentForDetails(int id)
        {
            var data = _productCommentRepository.GetCommentWithIgnore(id);
            return new DetailsCommentDto()
            {
                Comment = data.Comment,
                CreateDate = data.CreateDate,
                CommentId = data.CommentId,
                IsShow = data.IsShow
            };
        }

        public EditCommentDto GetCommentForEdit(int id)
        {
            var data = _productCommentRepository.GetCommentWithIgnore(id);
            return new EditCommentDto()
            {
                Comment = data.Comment,
                CreateDate = data.CreateDate,
                CommentId = data.CommentId,
                IsShow = data.IsShow
            };
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
