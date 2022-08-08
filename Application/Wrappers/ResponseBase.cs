using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class ResponseBase
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public int StatusCode { get; set; }
        public void setResponseMessage(bool succed, string message, int code)
        {
            Succeeded = succed;
            Message = message;
            StatusCode = code;
        }
        public void SetErrorMessage(string error, bool succeed,string message, int statusCode)
        {
            Error = error;           
            Message = message;
            StatusCode = statusCode;
            Succeeded = succeed;
        }
    }
}
