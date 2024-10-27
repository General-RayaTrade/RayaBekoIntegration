using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models.Responses
{
    public interface IStatus
    {
        bool? status { get; set; }
    }

    public class D365_SalesOrderResponse : IStatus
    {
        public bool? status { get; set; }  // Changed from `status` to `Status` to match interface
        public string Magento_OrderNumber { get; set; }
        public string D365_OrderNumber { get; set; }
        public string Message { get; set; }
        public List<string> Items { get; set; }
    }

    public class D365_SalesOrderResponses : IStatus
    {
        public List<D365_SalesOrderResponse> D365_SalesOrderResponseList { get; set; }
        public bool? status { get; set; }
    }
}
