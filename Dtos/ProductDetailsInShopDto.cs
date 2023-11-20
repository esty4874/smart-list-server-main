public record ProductDetailsInShopDto :ProductDto
{

    public int ProductId { get; set; }

    public int ShopListId { get; set; }

    public int Amount { get; set; }

}
