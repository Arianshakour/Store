using Store.Domain.Dtoes.AdminPanel.Product;
using Store.Domain.Dtoes.AdminPanel.ProductGroup;
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
        ProductGroupViewModel GetProductGroup(int id);
        List<string> GetGroupTitlesById(List<int> groupIds);
        ProductViewModel GetProducts();
        void AddProduct(CreateProductDto create);
        DetailsProductDto DetailsProduct(int id);
        EditProductDto GetForEditProduct(int id);
        void UpdateProduct(EditProductDto edit);
        DeleteProductDto GetForDeleteProduct(int id);
        void DeleteProduct(DeleteProductDto delete);
        ProductViewModel ShowLastProduct();
        ProductViewModel GetPopularProducts();
        ProductViewModel ShowProduct(int id);
        ProductViewModel ShowAllProduct(string search, string type, string orderby,
            int startPrice, int endPrice, List<int> selectedGroups, int page, int pageSize);

        void AddProductGroup(CreateProductGroupDto create);
        EditProductGroupDto GetProductGroupForEdit(int id);
        void UpdateProductGroup(EditProductGroupDto edit);
    }
}
