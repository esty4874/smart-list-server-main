using System;
using System.Collections.Generic;

namespace smartList.Models;

public partial class ProductDetailsInShop
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int ShopListId { get; set; }

    public int Amount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ShopList ShopList { get; set; } = null!;
}
