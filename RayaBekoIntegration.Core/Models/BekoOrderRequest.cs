using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models
{
    public class BekoOrderRequest
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PaymentMethod { get; set; }
        public Pricing Pricing { get; set; }
        public BekoCustomer Customer { get; set; }
        public BekoAddress Address { get; set; }
        public List<Product> Products { get; set; }
    }

    public class BekoCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }

    public class BekoAddress
    {
        public string Building { get; set; }
        public string Apt { get; set; }
        public string Floor { get; set; }
        public string Street { get; set; }
        public string CityName { get; set; }
        public string NeighborhoodName { get; set; }
    }

    public class Pricing
    {
        public string DeliveryFees { get; set; }
        public string Discount { get; set; }
        public string SubTotal { get; set; }
        public string TotalPrice { get; set; }
    }

    public class Product
    {
        public string SkuId { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
    }


}
