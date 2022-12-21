using Blazored.LocalStorage.Serialization;
using Newtonsoft.Json;

namespace AcademicTimePlanner.JSONHandling
{
    /// <summary>
    /// Provides serialization and deserialization functionality
    /// to be used by <see cref="Blazored.LocalStorage"/>.
    /// </summary>
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public T? Deserialize<T>(string text)
            => JsonConvert.DeserializeObject<T>(text);

        public string Serialize<T>(T obj)
            => JsonConvert.SerializeObject(obj);
    }
}
