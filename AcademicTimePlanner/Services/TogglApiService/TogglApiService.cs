using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using AcademicTimePlanner.Services.TogglService;
using AcademicTimePlanner.Store.State.TogglSettings;
using Fluxor;

namespace AcademicTimePlanner.Services.TogglApiService;

public class TogglApiService : ITogglApiService
{
    private readonly IState<TogglSettingsState> _settingsState;
    private readonly HttpClient _httpClient;
    private const string UserAgent = "AcademicTimePlanner";

    public TogglApiService(HttpClient httpClient, IState<TogglSettingsState> settingsState)
    {
        _settingsState = settingsState;
        _httpClient = httpClient;
    }

    private void SetDefaultRequestHeaders()
    {
        var byteArray = Encoding.ASCII.GetBytes($"{_settingsState.Value.TogglApiKey}:api_token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }
    
    public async Task<TogglDetailResponse> GetDetailsAsync()
    {
        SetDefaultRequestHeaders();
        string json = await _httpClient.GetStringAsync($"https://api.track.toggl.com/reports/api/v2/details?user_agent={UserAgent}&workspace_id={_settingsState.Value.TogglWorkspaceId}");

        TogglDetailResponse togglDetailResponse = JsonSerializer.Deserialize<TogglDetailResponse>(json, new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
        return togglDetailResponse;
    }
    
    public async Task<TogglDetailResponse> GetDetailsSinceAsync(DateOnly since)
    {
        SetDefaultRequestHeaders();

        var sinceAsString = since.ToString("yyyy-MM-dd");
        var baseUri = new Uri("https://api.track.toggl.com/reports/api/v2/details");
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["user_agent"] = UserAgent;
        query["workspace_id"] = _settingsState.Value.TogglWorkspaceId;
        query["since"] = sinceAsString;
        var uriBuilder = new UriBuilder(baseUri)
        {
            Query = query.ToString()
        };
        var requestUri = uriBuilder.Uri.ToString();
        var json = await _httpClient.GetStringAsync(requestUri);
        
        TogglDetailResponse togglDetailResponse = JsonSerializer.Deserialize<TogglDetailResponse>(json, new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
        return togglDetailResponse;
    }
}