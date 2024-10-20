using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Service.Services
{
    public class OrderService : IOrderService
    {
        public async Task<SalesOrder> CreateOrderFromBekoAsync(BekoOrderRequest bekoOrder)
        {
            // Map BEKO order to Raya order
            SalesOrder salesOrder = MapBekoOrderToRayaOrder(bekoOrder);

            // Calling B2C Raya creation order API.
            var result = await CallingB2CRayaOrderCreationAPI(new D365_SalesOrder_Request
            {
                SalesOrder = new List<SalesOrder> { salesOrder }
            });
            return salesOrder;
        }

        public SalesOrder MapBekoOrderToRayaOrder(BekoOrderRequest bekoOrder)
        {
            // Extract customer data from BEKO JSON
            var customer = new ClsCustomer
            {
                FirstName = bekoOrder.Customer.FirstName,
                LastName = bekoOrder.Customer.LastName,
                Email = bekoOrder.Customer.Email,
                PhoneNumber = bekoOrder.Customer.Mobile
            };

            // Extract shipping address data from BEKO JSON
            var shippingAddress = new ShippingAddress
            {
                BuildingNumber = bekoOrder.Address.Building,
                AddressLine_1 = $"{bekoOrder.Address.Apt}, {bekoOrder.Address.Floor}",
                AddressLine_2 = bekoOrder.Address.Street,
                City = bekoOrder.Address.CityName,
                District = bekoOrder.Address.NeighborhoodName,
            };

            // Extract order items from BEKO JSON
            var orderItems = new List<OrderItem>();
            foreach (var product in bekoOrder.Products)
            {
                var orderItem = new OrderItem
                {
                    ItemCode = product.SkuId,
                    QtySold = product.Quantity,
                    UnitPrice = decimal.Parse(product.Price.Replace("EGP", "").Trim())
                };
                orderItems.Add(orderItem);
            }

            // Extract totals from BEKO JSON
            var totals = new Totals
            {
                base_total = double.Parse(bekoOrder.Pricing.SubTotal.Replace("EGP", "").Trim()),
                discount = double.Parse(bekoOrder.Pricing.Discount.Replace("EGP", "").Trim()),
                payment_fees = double.Parse(bekoOrder.Pricing.DeliveryFees.Replace("EGP", "").Trim()),
                grand_total = double.Parse(bekoOrder.Pricing.TotalPrice.Replace("EGP", "").Trim())
            };

            // Create the SalesOrder object
            var salesOrder = new SalesOrder
            {
                M_OrderNumber = bekoOrder.Id.ToString(),
                CreateDate = DateTime.Parse(bekoOrder.CreatedAt.ToString()),
                PaymentMethod = bekoOrder.PaymentMethod == "cash" ? "Cash" : "Paymob",
                Customer = new List<ClsCustomer> { customer },
                shipping_address = new List<ShippingAddress> { shippingAddress },
                OrderItems = orderItems,
                Totals = totals,
                Comment = "Beko Online Order",
                Source = "Beko",
            };

            return salesOrder;
        }
        private async Task<string> CallingB2CRayaOrderCreationAPI(D365_SalesOrder_Request _SalesOrder_Request)
        {
            Console.WriteLine(DateTime.Now.ToString() + " Post to Dx: " + _SalesOrder_Request.SalesOrder[0].M_OrderNumber);
            var client = new HttpClient(); // ideally this would be created from IHttpClientFactory
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            //var request = new HttpRequestMessage(HttpMethod.Post, "http://www.rayatrade.com/RayaB2C_API/api/B2C_Prod/Create_D365_SalesOrder_Json");
            var request = new HttpRequestMessage(HttpMethod.Post, "http://rayatrade.com/RayaB2C_API_STG/api/B2C_Staging/Create_D365_SalesOrder_Json");
            //var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:31015/api/B2C_Staging/Create_D365_SalesOrder_Json");

            request.Headers.Add("Authorization", "Bearer npOCJrfVPZa1oYFibdkvlf2HeFFn-soDiiF0HepgECTAN-GUP5bHKJVddcILEsXCK4xDITq4KUFtpBxgQs88q6LMFgCt0Udy9kOqVw2PazymmtLko5-OVK23trBB-7buwO-7IW2l1h-yNE2DG3Hk-5zl9aARCyGnLz6ekQYjDEwEq3t98Lu7XHUQn579vUzIyGkrq02s7Tlt9pf2dKCkQooVgzTI44xK3-t4BKqhSsJaFOGcFvSgjvNwXZWGqlME");
            var body = System.Text.Json.JsonSerializer.Serialize(_SalesOrder_Request);
            request.Content = new StringContent(body, null, "application/json");
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(DateTime.Now.ToString() + " Dx Order for : " + _SalesOrder_Request.SalesOrder[0].M_OrderNumber + " with result: " + result);

            return result;
        }
    }
}
