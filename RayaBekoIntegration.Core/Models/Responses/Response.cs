namespace RayaBekoIntegration.Core.Models.Responses
{
    public class Response<T> where T : class
    {
        public int StatusCode { get; set; }
        public T? Payload { get; set; }
        public string Message { get; set; }

        public Response()
        {
            StatusCode = 200; // Default to 200 OK
            Message = "Success";
        }

        public Response(int statusCode, T payload, string message = "Success")
        {
            StatusCode = statusCode;
            Payload = payload;
            Message = message;
        }
    }

}
