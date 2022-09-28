using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcademicTimePlanner.Services.TogglApiService;

public class TogglDetailResponseData
{
    public long Id { get; set; }
    [JsonPropertyName("tid")]
    public long? TaskId { get; set; }
    [JsonPropertyName("dur")]
    public int Duration { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("start")]
    public DateTime DateTime { get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalProperties { get; set; }
}