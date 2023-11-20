using SmartList.Contracts;
using smartList;
using System.Collections.Generic;

namespace SmartList.Services
{
    public class ProductsService : IProductsServiceService
    {
        private readonly SmartListContext _context;
        public ProductsService(SmartListContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetProductList()
        {
            var productList = await _context.Products.Select
                (product => new ProductDto()
                {
                    Id = product.Id,
                    CategoryId = product.CategoryId,
                    CompanyId = product.CompanyId??null,
                    ProductName = product.ProductName,
                    IsInPackage = product.IsInPackage,
                    AmountInPackage = product.AmountInPackage,
                    Img = product.Img
                }
                ).ToListAsync();
            return productList;
        }

        public async Task<ProductDto> AddProduct(ProductDto productDto)
        {
            var result = _context.Products.Add(new Product()
            {
                CategoryId = productDto.CategoryId,
                CompanyId = productDto.CompanyId ?? null,
                ProductName = productDto.ProductName,
                AmountInPackage = productDto.AmountInPackage,
                Img = productDto.Img,
                IsInPackage = productDto.IsInPackage,
                Weight = productDto.Weight,
                WeightType = productDto.WeightType
            });
            await _context.SaveChangesAsync();
            var product = result.Entity;
            return new ProductDto()
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                CompanyId = product.CompanyId,
                ProductName = product.ProductName,
                IsInPackage = product.IsInPackage,
                AmountInPackage = product.AmountInPackage,
                Img = product.Img
            };
        }

    }
}
