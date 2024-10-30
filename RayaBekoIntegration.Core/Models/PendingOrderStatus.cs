using System;
using System.Collections.Generic;

namespace RayaBekoIntegration.WebAPI;

public partial class PendingOrderStatus
{
    public int Id { get; set; }

    public string RayaOrderNumber { get; set; } = null!;

    public string BekoOrderNumber { get; set; } = null!;

    public string RayaOrderStatus { get; set; } = null!;

    public DateTime? CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }
}
