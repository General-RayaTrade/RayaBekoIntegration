using RayaBekoIntegration.Core.Models;

namespace RayaBekoIntegration.Core.Models.Responses
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
