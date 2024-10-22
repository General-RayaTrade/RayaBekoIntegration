using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Service.Services
{
    public class ResponseService<T> : IResponseService<T> where T : class
    {
        public Response<T> CreateResponse(int statusCode, T? payload, string message = "Success")
        {
            return new Response<T>()
            {
                Message = message,
                StatusCode = statusCode,
                Payload = payload
            };
        }
    }
}
