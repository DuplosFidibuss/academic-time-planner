using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Services.TogglService;

public interface ITogglService
{
    Task<List<TogglEntrySum>> GetTogglEntrySumAsync(DateOnly since);
}