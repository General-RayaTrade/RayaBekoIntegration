using System;
using System.Collections.Generic;

namespace RayaBekoIntegration.EF;

public partial class OrderStatusLog
{
    public int Id { get; set; }

    public string BekoOrderNumber { get; set; } = null!;

    public string RayaOrderNumber { get; set; } = null!;

    public string BekoOrderStatus { get; set; } = null!;

    public string RayaOrderStatus { get; set; } = null!;

    public DateTime? CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }
}
