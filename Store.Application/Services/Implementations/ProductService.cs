using Microsoft.AspNetCore.Http;
using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel.Product;
using Store.Domain.Entities;
using Store.Domain.ViewModels;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public void AddProduct(CreateProductDto create)
        {
            create.ImageName = Guid.NewGuid() + Path.GetExtension(create.ImgUp.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SiteQaleb/UserAvatar", create.ImageName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                create.ImgUp.CopyTo(stream);
            }
            var p = new Product()
            {
                ProductTitle = create.ProductTitle,
                GroupId = create.GroupId,
                SubGroup = create.SubGroup,
                Price = create.Price,
                Mojodi = create.Mojodi,
                CreateDate = DateTime.Now,
                UserId = create.UserId,
                Tags = create.Tags,
                IsValid = create.IsValid,
                Dlt = false,
                ImageName = create.ImageName
            };
            _productRepository.AddProduct(p);
            _productRepository.Save();
        }

        public ProductGroupViewModel GetProductGroups()
        {
            var data = _productRepository.GetProductGroups();
            return new ProductGroupViewModel()
            {
                productGroupList = data
            };
        }

        public ProductGroupViewModel GetProductGroupsParent()
        {
            var data = _productRepository.GetProductGroups().Where(x => x.ParentId == null).ToList();
            return new ProductGroupViewModel()
            {
                productGroupList = data
            };
        }

        public ProductGroupViewModel GetProductGroupsSub(int gid)
        {
            var data = _productRepository.GetProductGroups().Where(x => x.ParentId == gid).ToList();
            return new ProductGroupViewModel()
            {
                productGroupList = data
            };
        }

        public ProductViewModel GetProducts()
        {
            var data = _productRepository.GetProducts();
            return new ProductViewModel()
            {
                productList = data
            };
        }
    }
}
