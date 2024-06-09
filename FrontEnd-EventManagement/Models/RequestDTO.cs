using static FrontEnd_EventManagement.Utility.ApiType;

namespace FrontEnd_EventManagement.Models
{
    public class RequestDTO
    {
        public ApiTypes ApiType { get; set; } = ApiTypes.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
