using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    [FeatureState]
    public class ProjectLinkerState
    {
        public bool Loaded { get; }

        public DisplayData? ProjectsData { get; }

        private ProjectLinkerState() { }

        public ProjectLinkerState(bool loaded, Data.DisplayData chartData)
        {
            Loaded = loaded;
            ProjectsData = chartData;
        }
    }
}
