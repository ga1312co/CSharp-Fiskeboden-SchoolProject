using System;
using System.Collections.Generic;

namespace Informatics.FiskeBoden.Models;

public partial class SupplierPickupPoint
{
    public int SupplierId { get; set; }

    public int PickupPointId { get; set; }

    public int SupplierPickupPointId { get; set; }

    public byte PickupDay { get; set; }

    public TimeOnly PickupTime { get; set; }

    public virtual PickupPoint PickupPoint { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
