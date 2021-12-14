using System.Net;

namespace Skinet.BusinessLogic.Core.Error
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        protected virtual string GetDefaultMessage(int statuCode)
        {
            return statuCode switch
            {
                (int)HttpStatusCode.BadRequest => "A bad request, you have made",
                (int)HttpStatusCode.Unauthorized => "You are not authorized",
                (int)HttpStatusCode.NotFound => "resource was not found",
                (int)HttpStatusCode.InternalServerError => "There is an internal server error",
                _ => null
            };
        }
    }
}
