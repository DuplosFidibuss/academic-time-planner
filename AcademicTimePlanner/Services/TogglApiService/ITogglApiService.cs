using AcademicTimePlanner.Services.TogglService;

namespace AcademicTimePlanner.Services.TogglApiService;

public interface ITogglApiService
{
    Task<TogglDetailResponse> GetDetailsAsync();
    Task<TogglDetailResponse> GetDetailsSinceAsync(DateOnly since);
}