using AcademicTimePlanner.Services.TogglApiService;
using AcademicTimePlanner.DataMapping.Toggl;

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

        togglDetailResponseWithSinceDate.Data.ForEach(response => {

            var togglProjectId = response.ProjectId.GetValueOrDefault(TogglProject.NoTogglProjectId);
            var togglTaskId = response.TaskId.GetValueOrDefault(TogglTask.NoTogglTaskId);

            var togglProject = togglProjects.FindLast(togglProject => togglProject.TogglId == togglProjectId); 
            if (togglProject == null)
            {
                togglProject = new TogglProject(togglProjectId, response.Project);
                togglProjects.Add(togglProject);
            }

            var togglTask = togglProject.GetOrCreateTogglTask(togglTaskId, response.Task);
            togglTask.AddEntry(new TogglEntrySum(response.StartTime, response.Duration/(double)3600000, response.Id, togglTaskId));
        });

        return togglProjects;
    }
}