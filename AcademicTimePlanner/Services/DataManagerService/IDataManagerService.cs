using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Services.DataManagerService
{
    public interface IDataManagerService
    {
        public Task<bool> SetTogglProjects(List<TogglProject> togglProjects);
    }
}
