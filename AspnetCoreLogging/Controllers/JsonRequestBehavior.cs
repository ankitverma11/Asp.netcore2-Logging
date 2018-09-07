using Newtonsoft.Json;

namespace AspnetCoreLogging.Controllers
{
    internal class JsonRequestBehavior
    {
        public static JsonSerializerSettings AllowGet { get; internal set; }
    }
}