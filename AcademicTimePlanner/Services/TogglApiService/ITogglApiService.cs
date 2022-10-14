namespace AcademicTimePlanner.Services.TogglApiService;

/// <summary>
/// Fetches Toggl data from the Toggl API.
/// </summary>
public interface ITogglApiService
{
    /// <summary>
    /// Fetches all raw Toggl data available in the given context.
    /// </summary>
    /// <returns><see cref="TogglDetailResponse"/> containing all fetched raw entries</returns>
    Task<TogglDetailResponse> GetDetailsAsync();

    /// <summary>
    /// Fetches all raw Toggl data available in the given context from the given
    /// date until calling time.
    /// </summary>
    /// <param name="since"></param>
    /// <returns><see cref="TogglDetailResponse"/> containing all fetched raw entries</returns>
    Task<TogglDetailResponse> GetDetailsSinceAsync(DateOnly since);
}