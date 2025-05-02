using Store.Domain.Dtoes.AdminPanel.Product;
using Store.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Interfaces
{
    public interface IProductService
    {
        ProductGroupViewModel GetProductGroups();
        ProductGroupViewModel GetProductGroupsParent();
        ProductGroupViewModel GetProductGroupsSub(int gid);
        ProductViewModel GetProducts();
        void AddProduct(CreateProductDto create);
        DetailsProductDto DetailsProduct(int id);
        EditProductDto GetForEditProduct(int id);
        void UpdateProduct(EditProductDto edit);
        DeleteProductDto GetForDeleteProduct(int id);
        void DeleteProduct(DeleteProductDto delete);
    }
}
