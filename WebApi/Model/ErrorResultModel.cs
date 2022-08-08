using System.Text.Json;

namespace WebApi.Model
{
    public class ErrorResultModel
    {
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
      

    }
}
