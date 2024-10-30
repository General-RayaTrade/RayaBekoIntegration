using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models.Responses
{
    public class UpdateOrderStatusResponse : IStatus
    {
        public string BekoOrderId { get; set; }
        public string OrderStatus { get; set; }
        public bool? status {  get; set; }
    }

}
