using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    [FeatureState]
    public class ProjectLinkerState
    {
        public bool Loaded { get; }

        public Data.Data? ChartData { get; }

        private ProjectLinkerState() { }

        public ProjectLinkerState(bool loaded, Data.Data chartData)
        {
            Loaded = loaded;
            ChartData = chartData;
        }
    }
}
