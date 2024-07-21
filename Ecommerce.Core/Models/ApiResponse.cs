using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class ApiResponse
    {
        public int? StatusCode { get; set; }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public object? Result {get;set;}

        public ApiResponse(int? statusCode = null, string? message = null , object result = null)
        {
            StatusCode = statusCode;
            Message = message ?? getMessageFromStatusCode(statusCode);
            Result = result;
            IsSuccess = statusCode >= 200 && statusCode <= 300;
        }

        private string? getMessageFromStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                200 => "successfully",
                400 => "Bad Request",
                404 => "not found",
                500 => "internal server error",
                 _ => null,
            } ;

        }
    }


    
}
