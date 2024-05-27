using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.Validation
{
    public class Response
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public object Result { get; set; }  


        public static ObjectResult ObjectResult (HttpStatusCode httpCode, bool success, string message, string code, object result)
        {
            Response response = new() {
                IsSuccess = success,
                Message = message,
                Code = code,
                Result = result
            };

            ObjectResult obj = new(response) {
                StatusCode = (int)httpCode
            };

            return obj;
        }
    }
}
