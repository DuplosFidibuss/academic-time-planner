using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    [FeatureState]
    public class ChartsState
    {
        public bool Loaded { get; }

        public ChartData? ChartData { get; }

        private ChartsState() { }

        public ChartsState(bool loaded, ChartData chartData)
        {
            Loaded = loaded;
            ChartData = chartData;
        }
    }
}
