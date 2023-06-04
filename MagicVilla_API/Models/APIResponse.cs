using System.Net;

namespace MagicVilla_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool isSuccess { get; set; } = true;

        public List<String> ErrorMessagge { get; set;}

        public Object Result { get; set; }
    }
}
