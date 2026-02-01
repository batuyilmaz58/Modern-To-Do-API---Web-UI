using System.Text.Json.Serialization;

namespace todoappjs.Models
{
    public class ServiceResponse<T> : BaseResponse
    {
        [JsonPropertyName("Entity")]
        public T Entity { get; set; }

        [JsonPropertyName("Entities")]
        public List<T> Entities { get; set; }

        public ServiceResponse()
        {
            Errors = new List<string>();
            Entities = new List<T>();
            Message = "";        
        }
    }
}
