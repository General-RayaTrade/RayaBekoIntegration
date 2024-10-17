using Newtonsoft.Json;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.WebAPI.ResonsesModelViews;

namespace RayaBekoIntegration.Service.Services
{
    public class StockService : IStockService
    {
        public async Task<IList<ItemInventoryQuantity>?> GetStockForProductAsync(string productItemCode)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://www.rayatrade.com/RayaB2C_API/api/B2C_Prod/GetStock?SKU={productItemCode}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            string jsonResponse = await response.Content.ReadAsStringAsync();
            B2CRayaServiceStockResponse stockResponse = JsonConvert.DeserializeObject<B2CRayaServiceStockResponse>(jsonResponse)!;


            var itemInventoryQuantities = stockResponse?.Products
                .Where(product => product?.Stocks != null)
                .SelectMany(product => product.Stocks
                    .Where(stock => stock != null && stock.quantity > 0)
                    .Select(stock => new ItemInventoryQuantity
                    {
                        inventory = stock.inventory,
                        quantity = stock.quantity
                    }))
                .ToList();

            return itemInventoryQuantities ?? new List<ItemInventoryQuantity>();
        }
    }
}
