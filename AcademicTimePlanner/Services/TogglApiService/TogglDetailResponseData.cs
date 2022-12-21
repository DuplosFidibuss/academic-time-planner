using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AcademicTimePlanner.Services.TogglApiService
{
    public class TogglDetailResponseData
    {
        public long Id { get; set; }

        public long? Pid { get; set; }

        public long? Tid { get; set; }

        public int Dur { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Project { get; set; }

        public string Task { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JToken> AdditionalProperties { get; set; }
    }
}