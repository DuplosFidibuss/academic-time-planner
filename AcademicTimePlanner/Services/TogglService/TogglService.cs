using AcademicTimePlanner.ApplicationData.Toggl;
using AcademicTimePlanner.Services.TogglApiService;

namespace AcademicTimePlanner.Services.TogglService;

public class TogglService : ITogglService
{
    private ITogglApiService _togglApiService;

    public TogglService(ITogglApiService togglApiService)
    {
        _togglApiService = togglApiService;
    }

    public async Task<List<TogglProject>> GetTogglProjects(DateOnly since)
    {
        TogglDetailResponse togglDetailResponseWithSinceDate = await _togglApiService.GetDetailsSinceAsync(since);
        var togglProjects = new List<TogglProject>();

        togglDetailResponseWithSinceDate.Data.ForEach(response =>
        {

            var togglProjectId = response.Pid.GetValueOrDefault(TogglProject.NoTogglProjectId);
            var togglTaskId = response.Tid.GetValueOrDefault(TogglProject.NoTogglProjectId);

            var togglProject = togglProjects.FindLast(togglProject => togglProject.TogglId == togglProjectId);
            if (togglProject == null)
            {
                togglProject = new TogglProject(togglProjectId, response.Project != null ? response.Project : "Entries without project");
                togglProjects.Add(togglProject);
            }

            togglProject.AddTogglTask(togglTaskId, response.Task);
            togglProject.AddEntry(new TogglEntrySum(response.Start, response.Dur / (double) 3600000, response.Id, togglTaskId));
        });

        return togglProjects;
    }
}