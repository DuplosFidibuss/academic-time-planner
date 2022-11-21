using AcademicTimePlanner.Store.State.Toggl;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace AcademicTimePlanner.Services.TogglApiService;

public class TogglApiService : ITogglApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private const string UserAgent = "AcademicTimePlanner";

    public TogglApiService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    private async void SetDefaultRequestHeaders()
    {
        var togglSettings = await _localStorageService.GetItemAsync<TogglSettings>(nameof(TogglSettings));
        var byteArray = Encoding.ASCII.GetBytes($"{togglSettings.TogglApiKey}:api_token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }

    public async Task<TogglDetailResponse> GetDetailsAsync()
    {
        SetDefaultRequestHeaders();
        var togglSettings = await _localStorageService.GetItemAsync<TogglSettings>(nameof(TogglSettings));
        string json = await _httpClient.GetStringAsync($"https://api.track.toggl.com/reports/api/v2/details?user_agent={UserAgent}&workspace_id={togglSettings.TogglWorkspaceId}");

        TogglDetailResponse togglDetailResponse = JsonConvert.DeserializeObject<TogglDetailResponse>(json);
        return togglDetailResponse;
    }

    public async Task<TogglDetailResponse> GetDetailsSinceAsync(DateOnly since)
    {
        SetDefaultRequestHeaders();
        var togglSettings = await _localStorageService.GetItemAsync<TogglSettings>(nameof(TogglSettings));

        var sinceAsString = since.ToString("yyyy-MM-dd");
        var baseUri = new Uri("https://api.track.toggl.com/reports/api/v2/details");
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["user_agent"] = UserAgent;
        query["workspace_id"] = togglSettings.TogglWorkspaceId;
        query["since"] = sinceAsString;
        var uriBuilder = new UriBuilder(baseUri)
        {
            Query = query.ToString()
        };
        var requestUri = uriBuilder.Uri.ToString();
        var json = await _httpClient.GetStringAsync(requestUri);

        TogglDetailResponse togglDetailResponse = JsonConvert.DeserializeObject<TogglDetailResponse>(json);
        return togglDetailResponse;
    }
}