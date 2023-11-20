namespace SmartList.Contracts
{
    public interface IShopListService
    {
        Task<bool> CreateListShop(ShopListDto shopListDtos);
        Task<ShopListDto> GetShopListSatistic(int userId);


    }
}
