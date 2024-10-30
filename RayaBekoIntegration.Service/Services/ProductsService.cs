using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.EF.IRepositories;
using RayaBekoIntegration.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Service.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VWbeko?> GetProdcutDetails(string SKU)
        {
            var result = await _unitOfWork.vWBekos.FindAsync(pro => pro.Sku == SKU);
            if (result == null)
            {
                throw new Exception($"Invalid Item code {SKU}.");
            }
            return result;
        }

        public IList<VWbeko> GetProdcutsDetails()
        {
            return _unitOfWork.vWBekos.GetAll().ToList();
        }
    }
}
