using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.EF;
using RayaBekoIntegration.EF.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Service.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DistrictService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<VWcityDistrict> GetAllCityDistricts()
        {
            return _unitOfWork.vWCityDistricts.GetAll();
        }
    }
}
