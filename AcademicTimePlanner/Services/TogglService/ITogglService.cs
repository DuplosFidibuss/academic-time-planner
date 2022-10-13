using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Services.TogglService;

/// <summary>
/// Proceeds Toggl raw data to ATP-conform Toggl data.
/// </summary>
public interface ITogglService
{
    /// <summary>
    /// Converts all Toggl raw entries from the given date until calling time to
    /// <see cref="TogglEntrySum"/>s and groups them into <see cref="TogglProject"/>s.
    /// </summary>
    /// <param name="since"></param>
    /// <returns>A list containing the created<see cref="TogglProject"/>s</returns>
    Task<List<TogglProject>> GetTogglProjects(DateOnly since);
}