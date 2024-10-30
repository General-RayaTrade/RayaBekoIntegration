using System;
using System.Collections.Generic;

namespace RayaBekoIntegration.WebAPI;

public partial class VWbeko
{
    public int Id { get; set; }

    public string Sku { get; set; } = null!;

    public string MainImage { get; set; } = null!;

    public string? AdditionalImage { get; set; }

    public string? SkuImage { get; set; }

    public string ModelImageUrl { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public string Skuname { get; set; } = null!;

    public string? CategoryName { get; set; }

    //public string? SubsidyCat { get; set; }

    public string ItemCode { get; set; } = null!;

    public string RColor { get; set; } = null!;

    public decimal? QtyAvailphysical { get; set; }

    public decimal? Qty { get; set; }

    public string RBrand { get; set; } = null!;

    public long Product { get; set; }

    public string Itemid { get; set; } = null!;

    public string Namealias { get; set; } = null!;

    public string? DepName { get; set; }

    public string? CatName { get; set; }

    public string Brand { get; set; } = null!;

    public string SkuModel { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string ColorAr { get; set; } = null!;

    public decimal? Amount { get; set; }

    public string? ItemType { get; set; }

    public string? ItemTax { get; set; }

    public string SkuBrand { get; set; } = null!;

    public string SkuColor { get; set; } = null!;

    public string RBrickcode { get; set; } = null!;

    public string ROrainvitemid { get; set; } = null!;

    public DateTime Createddatetime { get; set; }

    public DateTime Modifieddatetime { get; set; }

    public string? ReportMainCategoryEn { get; set; }

    public string? ReportMainCategoryAr { get; set; }

    public string ShortDescriptionAr { get; set; } = null!;

    public string ShortDescriptionEn { get; set; } = null!;

    public int Weight { get; set; }

    public string Inventlocationid { get; set; } = null!;

    public string? MenuItemAr { get; set; }

    public string MenuItemEn { get; set; } = null!;
}
