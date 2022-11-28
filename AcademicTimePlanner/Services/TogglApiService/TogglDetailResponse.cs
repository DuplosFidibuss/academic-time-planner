using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AcademicTimePlanner.Services.TogglApiService;

public class TogglDetailResponse
{
    public List<TogglDetailResponseData> Data { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JToken> AdditionalProperties { get; set; }
}