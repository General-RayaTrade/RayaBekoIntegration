using RayaBekoIntegration.Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.IServices
{
    public  interface IResponseService<T> where T : class
    {
        Response<T> CreateResponse(int statusCode, T? payload, string message = "Success");
    }
}
