using smartList.Models;
using System.Net.Http.Headers;

namespace SmartList.Services
{
    public class ShopListService : IShopListService
    {
        private readonly SmartListContext _context;
        public ShopListService(SmartListContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateListShop(ShopListDto shopListDto)
        {
            try
            {

                var shopList = _context.ShopLists.Add(new ShopList()
                {
                    Date = DateTime.Now,
                    UserId = shopListDto.UserId,
                    IsUsedSatistic=shopListDto.IsUsedSatistic,
                });

                await _context.SaveChangesAsync();

                foreach (var productDto in shopListDto.ProductDetailsInShops)
                {
                    var product = new ProductDetailsInShop
                    {
                        ShopListId = shopList.Entity.Id,
                        Amount = productDto.Amount,
                        ProductId = productDto.ProductId
                    };

                    _context.ProductDetailsInShops.Add(product);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<ShopListDto> GetShopListSatistic(int userId)
        {

            var userShopLists = _context.ShopLists
            .Where(s => s.UserId == userId&&s.IsUsedSatistic==true).OrderBy(s => s.Date);
            var countShopList = await userShopLists.CountAsync();
            var firstShopList = await userShopLists.FirstAsync();
            var lastShopList = await userShopLists.LastAsync();

            int daysDifference = (lastShopList.Date - firstShopList.Date).Days;
            var timeShopAvarge = daysDifference / countShopList;

            var productDetailsForUser = await _context.ProductDetailsInShops.Include(s => s.Product).Include(s => s.ShopList)
         .Where(p => p.ShopList.UserId == userId&& p.ShopList.IsUsedSatistic == true)
         .ToListAsync();

            var userProducts = productDetailsForUser
                .GroupBy(p => new { p.ProductId, p.Product })
                .Select(group => new
                {
                    Product = group.Key.Product,
                    ProductId = group.Key.ProductId,
                    TotalAmount = group.Sum(p => p.Amount) / countShopList
                })
                .ToList();
            List<ProductDetailsInShopDto> userNewProducts = new List<ProductDetailsInShopDto>();
            foreach (var userProduct in userProducts)
            {
                if (userProduct.TotalAmount <= countShopList)
                {
                    var productDetailsInShopDto = new ProductDetailsInShopDto()
                    {
                        ProductId = userProduct.ProductId,
                        //Amount = countShopList / userProduct.TotalAmount,
                        Amount = countShopList / userProduct.TotalAmount,
                        ShopListId = 0,
                        Id = userProduct.Product.Id,
                        CategoryId = userProduct.Product.CategoryId,
                        CompanyId = userProduct.Product.CompanyId,
                        ProductName = userProduct.Product.ProductName,
                        IsInPackage = userProduct.Product.IsInPackage,
                        AmountInPackage = userProduct.Product.AmountInPackage,
                        Weight = userProduct.Product.Weight,
                        WeightType = userProduct.Product.WeightType,
                        Img = userProduct.Product.Img

                    };
                    userNewProducts.Add(productDetailsInShopDto);
                }
                else
                {
                    var lastShopListWithProduct = await _context.ShopLists
           .Where(s => s.UserId == userId&&s.IsUsedSatistic==true)
           .OrderByDescending(s => s.Date)
           .Select(shopList => new
           {
               Date = shopList.Date,
               ShopList = shopList,
               ContainsProduct = _context.ProductDetailsInShops.Any(pd => pd.ShopListId == shopList.Id && pd.ProductId == userProduct.ProductId)
           })
           .FirstOrDefaultAsync(shopListWithProduct => shopListWithProduct.ContainsProduct);
                    if ((DateTime.Now - lastShopListWithProduct.Date).Days >= userProduct.TotalAmount)
                    {
                        var productDetailsInShopDto = new ProductDetailsInShopDto()
                        {
                            ProductId = userProduct.ProductId,
                            Amount = userProduct.TotalAmount,
                            ShopListId = 0,
                            Id = userProduct.Product.Id,
                            CategoryId = userProduct.Product.CategoryId,
                            CompanyId = userProduct.Product.CompanyId,
                            ProductName = userProduct.Product.ProductName,
                            IsInPackage = userProduct.Product.IsInPackage,
                            AmountInPackage = userProduct.Product.AmountInPackage,
                            Weight = userProduct.Product.Weight,
                            WeightType = userProduct.Product.WeightType,
                            Img = userProduct.Product.Img

                        };
                        userNewProducts.Add(productDetailsInShopDto);
                    }
                }
            }
            ShopListDto shopListDto = new ShopListDto
            {
                UserId = userId,
                Date = DateTime.Now,
                ProductDetailsInShops = userNewProducts
            };
            return shopListDto;

        }

    }
}
