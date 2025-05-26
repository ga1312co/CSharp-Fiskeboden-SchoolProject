using System;
using System.Collections.Generic;

namespace Informatics.FiskebodenWebAPI.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductNo { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public bool IsMeasuredInUnits { get; set; }

    public int CategoryId { get; set; }

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual Category Category { get; set; } = null!;
}
