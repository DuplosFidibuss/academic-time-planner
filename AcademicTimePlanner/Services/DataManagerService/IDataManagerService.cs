using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Services.DataManagerService
{
    /// <summary>
    /// Handles access to the <see cref="DataManager"/> of the current session.
    /// The <see cref="DataManager"/> can only be accessed via this service.
    /// </summary>
    public interface IDataManagerService
    {
        /// <summary>
        /// Updates the Toggl data hold by <see cref="DataManager"/>.
        /// </summary>
        /// <param name="togglProjects"></param>
        /// <returns><c>true</c> if the update was successful, otherwise <c>false</c>.</returns>
        public Task<bool> SetTogglProjects(List<TogglProject> togglProjects);
    }
}
