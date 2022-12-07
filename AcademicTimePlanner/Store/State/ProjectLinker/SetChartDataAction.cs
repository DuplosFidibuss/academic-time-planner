using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class SetProjectsDataAction
    {
        public DisplayData ProjectsData { get; set; }

        public SetProjectsDataAction(DisplayData projectsData)
        {
            ProjectsData = projectsData;
        }
    }
}
