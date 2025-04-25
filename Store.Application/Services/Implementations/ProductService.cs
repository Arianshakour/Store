using Store.Application.Services.Interfaces;
using Store.Domain.ViewModels;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductGroupViewModel GetProductGroups()
        {
            var data = _productRepository.GetProductGroups();
            return new ProductGroupViewModel()
            {
                productGroupList = data
            };
        }
    }
}
