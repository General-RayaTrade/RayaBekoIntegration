using RayaBekoIntegration.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.IServices
{
    public interface IProductsService
    {
        IList<VWbeko> GetProdcutsDetails();
        Task<VWbeko?> GetProdcutDetails(string SKU);
    }
}
