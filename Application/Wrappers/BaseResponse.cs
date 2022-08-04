using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Wrappers
{
    public class BaseResponse<T>
    {
       
            public BaseResponse()
            {
            }
            public BaseResponse(T body, string message = null)
            {
                Succeeded = true;
                Message = message;
                Body = body;
            }
            public BaseResponse(string message)
            {
                Succeeded = false;
                Message = message;
            }
         
            public bool Succeeded { get; set; }
            public string Message { get; set; }
            public string Errors { get; set; }
            public T Body { get; set; }
        }
    
}
