using System;
using System.Collections.Generic;

namespace RayaBekoIntegration.EF;

public partial class OrderDetail
{
    public int Id { get; set; }

    public string BekoOrderNumber { get; set; } = null!;

    public string RayaOrderNumber { get; set; } = null!;

    public string FilePath { get; set; } = null!;
}
