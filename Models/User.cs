using System;
using System.Collections.Generic;

namespace smartList.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ShopList> ShopLists { get; set; } = new List<ShopList>();
}
