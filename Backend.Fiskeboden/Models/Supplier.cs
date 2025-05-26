using System;
using System.Collections.Generic;

namespace Informatics.FiskebodenWebAPI.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierNo { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public string SupplierEmail { get; set; } = null!;

    public string? SupplierPhoneNumber { get; set; }

    public string? SupplierLocation { get; set; }

    public string SupplierSwishNumber { get; set; } = null!;

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual ICollection<SupplierPickupPoint> SupplierPickupPoints { get; set; } = new List<SupplierPickupPoint>();
}
