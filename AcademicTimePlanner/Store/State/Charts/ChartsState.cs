using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    [FeatureState]
    public class ChartsState
    {
        public bool Loaded { get; }

        public bool LoadedFiltered { get; }

        public ChartData? ChartData { get; }

        public DateFilter? DateFilter { get; }

        private ChartsState() { }

        public ChartsState(bool loaded, bool loadedFiltered, ChartData chartData, DateFilter dateFilter)
        {
            Loaded = loaded;
            LoadedFiltered = loadedFiltered;
            ChartData = chartData;
            DateFilter = dateFilter;
        }
    }
}
