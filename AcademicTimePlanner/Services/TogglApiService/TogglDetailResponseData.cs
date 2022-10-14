using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcademicTimePlanner.Services.TogglApiService;

public class TogglDetailResponseData
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("pid")]
    public long? ProjectId { get; set; }

    [JsonPropertyName("tid")]
    public long? TaskId { get; set; }

    [JsonPropertyName("dur")]
    public int Duration { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("start")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("end")]
    public DateTime EndTime { get; set; }

    [JsonPropertyName("project")]
    public string Project { get; set; }

    [JsonPropertyName("task")]
    public string Task { get; set; }
    
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalProperties { get; set; }
}