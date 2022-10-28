using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    [FeatureState]
    public class ChartsState
    {
        public bool Loaded { get; }

        public ChartData? ChartData { get; }

        public DateFilter? DateFilter { get; }

        private ChartsState() { }

        public ChartsState(bool loaded, ChartData chartData, DateFilter dateFilter)
        {
            Loaded = loaded;
            ChartData = chartData;
            DateFilter = dateFilter;
        }
    }
}
