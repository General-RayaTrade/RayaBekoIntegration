using RayaBekoIntegration.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.IServices
{
    public interface IDistrictService
    {
        IEnumerable<VWcityDistrict> GetAllCityDistricts();
    }
}
