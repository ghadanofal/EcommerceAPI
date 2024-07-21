using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO
{
    public class ApiValidationResponse : ApiResponse
    {
        //public int statusCode { get; set; }
        //public string message { get; set; } = null!;

        public IEnumerable<string> Errors { get; set; }

        //public  bool isSuceess { get; set; }
        public ApiValidationResponse(IEnumerable<string> errors, int? statusCode = 400):base(statusCode)
        {
            Errors = errors;
        }

    }
}
