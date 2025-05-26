using System;
using System.Collections.Generic;

namespace Informatics.FiskebodenWebAPI.Models;

public partial class Batch
{
    public int BatchId { get; set; }

    public string BatchNo { get; set; } = null!;

    public int SupplierId { get; set; }

    public int ProductId { get; set; }

    public int? BatchWeek { get; set; }

    public decimal? BatchQuantity { get; set; }

    public DateOnly ProductionDate { get; set; }

    public int BatchShelfLife { get; set; }

    public decimal BatchPrice { get; set; }

    public string BatchOrigin { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
