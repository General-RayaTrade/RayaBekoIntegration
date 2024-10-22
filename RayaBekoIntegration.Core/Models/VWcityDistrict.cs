using System;
using System.Collections.Generic;

namespace RayaBekoIntegration.EF;

public partial class VWcityDistrict
{
    public int Id { get; set; }

    public string DName { get; set; } = null!;

    public string? DNameAr { get; set; }

    public string? CNameAr { get; set; }

    public int? CityId { get; set; }
}
