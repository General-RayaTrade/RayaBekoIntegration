using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models.Responses
{
    public class D365_SalesOrderResponse
    {
        public bool status { get; set; }
        public string Magento_OrderNumber { get; set; }
        public string D365_OrderNumber { get; set; }
        public string Message { get; set; }
        public List<string> Items { get; set; }
    }
    public class D365_SalesOrderResponses
    {
        public List<D365_SalesOrderResponse> D365_SalesOrderResponseList { set; get; }
    }
}
