using Newtonsoft.Json;

namespace WebApi.Services.Utilities
{
    public static class Extensions
    {
        public static string ToJson<T>(this T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
