using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class SaveProjectsDataAction
    {
        public ProjectsData ProjectsData { get; }

        public SaveProjectsDataAction(ProjectsData projectsData)
        {
            ProjectsData = projectsData;
        }
    }
}
