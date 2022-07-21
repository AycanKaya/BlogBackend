using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.Wrappers
{
    public class ExceptionResponse : Exception
    {
        public ExceptionResponse() : base() { }

        public ExceptionResponse(string message) : base(message) { }

        public ExceptionResponse(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
