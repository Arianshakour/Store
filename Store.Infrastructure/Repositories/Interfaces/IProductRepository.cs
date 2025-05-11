using Azure;
using Store.Domain.Entities;
using Store.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<ProductGroup> GetProductGroups();
        List<Product> GetProducts();
        List<Product> GetLastProducts();
        List<Product> GetPopularProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void RemoveProduct(Product product);
        Product GetProductById(int id);
        ProductGroup GetProductGroupById(int id);
        void Save();
        List<Product> ShowAllProduct(string search,string type, string orderby,
            int startPrice, int endPrice, List<int> selectedGroups);
    }
}
