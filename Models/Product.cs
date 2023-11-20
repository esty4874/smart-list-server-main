using System;
using System.Collections.Generic;

namespace smartList.Models;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int? CompanyId { get; set; }

    public string ProductName { get; set; } = null!;

    public bool? IsInPackage { get; set; }

    public int? AmountInPackage { get; set; }

    public int? Weight { get; set; }

    public string WeightType { get; set; } = null!;

    public string? Img { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<ProductDetailsInShop> ProductDetailsInShops { get; set; } = new List<ProductDetailsInShop>();
}
