using System;
using System.Collections.Generic;

namespace smartList.Models;

public partial class ShopList
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }
    public bool? IsUsedSatistic { get; set; }


    public virtual ICollection<ProductDetailsInShop> ProductDetailsInShops { get; set; } = new List<ProductDetailsInShop>();

    public virtual User User { get; set; } = null!;
}
