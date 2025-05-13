using Microsoft.AspNetCore.Http;
using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel.Product;
using Store.Domain.Dtoes.AdminPanel.ProductGroup;
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

        public void AddProductGroup(CreateProductGroupDto create)
        {
            var pg = new ProductGroup()
            {
                GroupTitle = create.GroupTitle,
                ParentId = null,
                Dlt = false
            };
            _productRepository.AddProductGroup(pg);
            _productRepository.Save();
            if(create.subGroups != null && create.subGroups.Any())
            {
                foreach(var item in create.subGroups)
                {
                    var sg = new ProductGroup()
                    {
                        GroupTitle = item,
                        ParentId = pg.GroupId,
                        Dlt = false
                    };
                    _productRepository.AddProductGroup(sg);
                    _productRepository.Save();
                }
            }
        }

        public EditProductGroupDto GetProductGroupForEdit(int id)
        {
            var pg = _productRepository.GetProductGroupById(id);
            return new EditProductGroupDto()
            {
                Id = pg.GroupId,
                GroupTitle = pg.GroupTitle,
                SubGroups = pg.productGroups.Select(sg => new SubGroupDto
                {
                    Id = sg.GroupId,
                    GroupTitle = sg.GroupTitle
                }).ToList()
            };
        }

        public void UpdateProductGroup(EditProductGroupDto edit)
        {
            var parent = _productRepository.GetProductGroupById(edit.Id);
            parent.GroupTitle = edit.GroupTitle;
            //in khat zir mige ke onaei ke az form hazf kardio az database hazf kone
            var toRemove = parent.productGroups.Where(sg => !edit.SubGroups.Any(x => x.Id == sg.GroupId)).ToList();
            foreach(var i in toRemove)
            {
                _productRepository.DeleteProductGroup(i);
            }
            foreach (var item in edit.SubGroups)
            {
                if (item.Id.HasValue)
                {
                    //edit
                    var existing = _productRepository.GetProductGroupById(item.Id.Value);
                    existing.GroupTitle = item.GroupTitle;
                }
                else
                {
                    //add
                    var n = new ProductGroup()
                    {
                        GroupTitle = item.GroupTitle,
                        ParentId = parent.GroupId,
                        Dlt = false
                    };
                    _productRepository.AddProductGroup(n);
                }
            }
            _productRepository.Save();
        }
        public DeleteProductGroupDto GetProductGroupForDelete(int id)
        {
            var parent = _productRepository.GetProductGroupById(id);
            return new DeleteProductGroupDto()
            {
                Id = parent.GroupId,
                GroupTitle = parent.GroupTitle,
                SubGroups = parent.productGroups.Select(x => new SubGroupDtoForDelete()
                {
                    Id = x.GroupId,
                    GroupTitle = x.GroupTitle
                }).ToList()
            };
        }
        public void DeleteProductGroup(DeleteProductGroupDto delete)
        {
            var parent = _productRepository.GetProductGroupById(delete.Id);
            var subToRemove = parent.productGroups.Where(sg => delete.SubGroups.Any(x => x.Id == sg.GroupId)).ToList();
            foreach (var i in subToRemove)
            {
                _productRepository.DeleteProductGroup(i);
            }
            _productRepository.DeleteProductGroup(parent);
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

        public List<string> GetGroupTitlesById(List<int> groupIds)
        {
            var data = _productRepository.GetProductGroups();
            var dataJustTitle = data.Where(g => groupIds.Contains(g.GroupId))
            .Select(g => g.GroupTitle).ToList();
            return dataJustTitle;
        }

        public ProductViewModel GetPopularProducts()
        {
            var data = _productRepository.GetPopularProducts();
            return new ProductViewModel()
            {
                productList = data
            };
        }

        public ProductGroupViewModel GetProductGroup(int id)
        {
            var p = _productRepository.GetProductGroupById(id);
            return new ProductGroupViewModel()
            {
                productGroup = p
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

        public ProductViewModel ShowAllProduct(string search, string type, string orderby,
            int startPrice, int endPrice, List<int> selectedGroups, int page, int pageSize)
        {
            var data = _productRepository.ShowAllProduct(search, type, orderby, startPrice, endPrice, selectedGroups);
            int TotalRecord = data.Count();
            if (pageSize==-1)
            {
                return new ProductViewModel()
                {
                    productList = data,
                    pager = new Pager(TotalRecord, page, TotalRecord)
                };
            }
            return new ProductViewModel()
            {
                productList = data.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                pager = new Pager(TotalRecord, page, pageSize)
            };
        }

        public ProductViewModel ShowLastProduct()
        {
            var data = _productRepository.GetLastProducts();
            return new ProductViewModel()
            {
                productList = data
            };
        }

        public ProductViewModel ShowProduct(int id)
        {
            var p = _productRepository.GetProductById(id);
            return new ProductViewModel()
            {
                product = p
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

        public List<string> GetSearchSuggestions(string search)
        {
            var data = _productRepository.GetSearchSuggestions(search);
            return data.Select(x => x.ProductTitle).ToList();
        }
    }
}
