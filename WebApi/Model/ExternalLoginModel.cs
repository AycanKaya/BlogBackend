using Newtonsoft.Json;

namespace WebApi.Model
{
    public class ExternalLoginModel
    {
        [JsonProperty("resultCode")]
        public string resultCode { get; set; }


        [JsonProperty("resultDescription")]
        public string resultDescription { get; set; }
    }
}
