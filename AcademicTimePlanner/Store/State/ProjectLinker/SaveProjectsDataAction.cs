using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class SaveProjectsDataAction
    {
        public DisplayData ProjectsData { get; }

        public SaveProjectsDataAction(DisplayData projectsData)
        {
            ProjectsData = projectsData;
        }
    }
}
