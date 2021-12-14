using System.Collections.Generic;
using System.Net;

namespace Skinet.BusinessLogic.Core.Error
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base((int)HttpStatusCode.UnprocessableEntity)
        {
            Message = GetDefaultMessage(StatusCode);
        }

        protected override string GetDefaultMessage(int statuCode)
        {
            return statuCode switch
            {
                (int)HttpStatusCode.UnprocessableEntity => "Inputs are invalid",
                _ => null
            };
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
