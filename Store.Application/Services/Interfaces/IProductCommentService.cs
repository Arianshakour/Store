using Store.Domain.Dtoes.AdminPanel.ProductComment;
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
        DetailsCommentDto GetCommentForDetails(int id);
        EditCommentDto GetCommentForEdit(int id);
        void EditComment(EditCommentDto edit);
        DeleteCommentDto GetCommentForDelete(int id);
        void DeleteComment(DeleteCommentDto delete);
    }
}
