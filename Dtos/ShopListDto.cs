public record ShopListDto
{
    public int UserId { get; set; }

    public DateTime Date { get; set; }
    public bool? IsUsedSatistic { get; set; }

    public List<ProductDetailsInShopDto> ProductDetailsInShops { get; set; } = new List<ProductDetailsInShopDto>();

}
