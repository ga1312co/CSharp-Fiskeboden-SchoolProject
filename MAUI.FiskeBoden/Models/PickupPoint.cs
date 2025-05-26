using System;
using System.Collections.Generic;

namespace Informatics.FiskeBoden.Models;

public partial class PickupPoint
{
    public int PickupPointId { get; set; }

    public string PickupPointNo { get; set; } = null!;

    public string PickupPointAddress { get; set; } = null!;

    public string PickupPointName { get; set; } = null!;

    public virtual ICollection<SupplierPickupPoint> SupplierPickupPoints { get; set; } = new List<SupplierPickupPoint>();
}
