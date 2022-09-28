using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcademicTimePlanner.Services.TogglApiService;

public class TogglDetailResponse
{
    public List<TogglDetailResponseData> Data { get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalProperties { get; set; }
}