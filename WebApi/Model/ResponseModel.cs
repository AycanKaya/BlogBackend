using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class ResponseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
