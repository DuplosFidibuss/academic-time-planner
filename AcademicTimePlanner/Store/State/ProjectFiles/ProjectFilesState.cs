using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    [FeatureState]
    public class ProjectFilesState
    {
        public bool Loaded { get; }

        public int NumberOfPlanProjects { get; }

        private ProjectFilesState() { }

        public ProjectFilesState(bool loaded, int numberOfPlanProjects)
        {
            Loaded = loaded;
            NumberOfPlanProjects = numberOfPlanProjects;
        }
    }
}
