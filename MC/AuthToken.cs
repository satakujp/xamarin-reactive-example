
using Newtonsoft.Json;

namespace MC
{
    public class AuthToken
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }

    public static class ObjectExtensions
    {
        public static string toJSON(this object obj)
        {
			return JsonConvert.SerializeObject(obj);
        }
    }
}

