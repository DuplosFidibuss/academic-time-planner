using Blazored.LocalStorage.Serialization;
using Newtonsoft.Json;

namespace AcademicTimePlanner.JSONHandling
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public T? Deserialize<T>(string text)
        => JsonConvert.DeserializeObject<T>(text);

        public string Serialize<T>(T obj)
            => JsonConvert.SerializeObject(obj);
    }
}
