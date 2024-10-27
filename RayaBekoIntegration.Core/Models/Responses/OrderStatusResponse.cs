using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models.Responses
{
    public class Item
    {
        public string partnerOrderId { get; set; }
        public string trackingNumber { get; set; }
        public string carrier { get; set; }
        public string lineItemId { get; set; }
        public string sku { get; set; }
        public int quantityOrdered { get; set; }
        public int quantityShipped { get; set; }
        public int quantityReturned { get; set; }
        public string status { get; set; }
    }

    public class OrderStatusResponse : IStatus
    {
        public string BekoOrderId { get; set; }
        public List<Item> items { get; set; }
        public bool? status {  get; set; }
    }

}
