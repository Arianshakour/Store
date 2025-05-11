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
    }
}
