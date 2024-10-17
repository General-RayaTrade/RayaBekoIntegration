using RayaBekoIntegration.Core.Models;

namespace RayaBekoIntegration.WebAPI.ResonsesModelViews
{
    public class B2CRayaServiceStockResponse
    {
        public List<Product> Products { get; set; }
    }
    public class Product
    {
        public string SKU { get; set; }
        public List<ItemInventoryQuantity> Stocks { get; set; }
    }
}
