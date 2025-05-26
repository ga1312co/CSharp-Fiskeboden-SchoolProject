using System;
using System.Collections.Generic;

namespace Informatics.FiskeBoden.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryNo { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
