using RayaBekoIntegration.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models.Responses
{
    public class ProductsDetailsResponse : IStatus
    {
        public IList<VWbeko> products { get; set; }
        public bool? status { get; set; }
    }
}
