using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    [FeatureState]
    public class ProjectLinkerState
    {
        public bool Loaded { get; }

        public ChartData? ChartData { get; }

        private ProjectLinkerState() { }

        public ProjectLinkerState(bool loaded, ChartData chartData)
        {
            Loaded = loaded;
            ChartData = chartData;
        }
    }
}
