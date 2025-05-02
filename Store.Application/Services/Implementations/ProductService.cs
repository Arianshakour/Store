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

        public void DeleteProduct(DeleteProductDto delete)
        {
            var p = _productRepository.GetProductById(delete.ProductId);
            p.Dlt = true;
            _productRepository.UpdateProduct(p);
            _productRepository.Save();
        }

        public DetailsProductDto DetailsProduct(int id)
        {
            var p = _productRepository.GetProductById(id);
            return new DetailsProductDto()
            {
                ProductId = p.ProductId,
                ProductTitle = p.ProductTitle,
                CreateDate = p.CreateDate,
                GroupId = p.GroupId,
                SubGroup = p.SubGroup,
                Price = p.Price,
                Mojodi = p.Mojodi,
                UserId = p.UserId,
                Tags = p.Tags,
                IsValid = p.IsValid,
                ImageName = p.ImageName
            };
        }

        public DeleteProductDto GetForDeleteProduct(int id)
        {
            var p = _productRepository.GetProductById(id);
            return new DeleteProductDto()
            {
                ProductId = p.ProductId,
                ProductTitle = p.ProductTitle,
                GroupId = p.GroupId,
                Price = p.Price,
                Mojodi = p.Mojodi,
                IsValid = p.IsValid,
            };
        }

        public EditProductDto GetForEditProduct(int id)
        {
            var p = _productRepository.GetProductById(id);
            return new EditProductDto()
            {
                ProductId = p.ProductId,
                ProductTitle = p.ProductTitle,
                GroupId = p.GroupId,
                SubGroup = p.SubGroup,
                Price = p.Price,
                Mojodi = p.Mojodi,
                Tags = p.Tags,
                IsValid = p.IsValid,
                ImageName = p.ImageName
            };
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

        public void UpdateProduct(EditProductDto edit)
        {
            var p = _productRepository.GetProductById(edit.ProductId);
            p.ProductTitle = edit.ProductTitle;
            p.GroupId = edit.GroupId;
            p.SubGroup = edit.SubGroup;
            p.Mojodi = edit.Mojodi;
            p.Price = edit.Price;
            p.IsValid = edit.IsValid;
            if (edit.imgUp != null)
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SiteQaleb/UserAvatar", edit.ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                edit.ImageName = Guid.NewGuid() + Path.GetExtension(edit.imgUp.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SiteQaleb/UserAvatar", edit.ImageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    edit.imgUp.CopyTo(stream);
                }
            }
            p.ImageName = edit.ImageName;
            p.Tags = edit.Tags;
            _productRepository.UpdateProduct(p);
            _productRepository.Save();
        }
    }
}
