using RayaBekoIntegration.Core.Models;
using System.Text.Json;

namespace RayaBekoIntegration.WebAPI.ResonsesModelViews
{
    public class ProductStockResponse
    {
        public string productItemCode {  get; set; }
        public List<ItemInventoryQuantity> Details { get; set; } // List of JSON strings
    }
}
