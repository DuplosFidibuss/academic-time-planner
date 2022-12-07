using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    [FeatureState]
    public class ChartsState
    {
        public bool Loaded { get; set; }

        public bool LoadedFiltered { get; }

        public Data.Data? ChartData { get; }

        public DateFilter? DateFilter { get; }

        private ChartsState() { }

        public ChartsState(bool loaded, bool loadedFiltered, Data.Data chartData, DateFilter dateFilter)
        {
            Loaded = loaded;
            LoadedFiltered = loadedFiltered;
            ChartData = chartData;
            DateFilter = dateFilter;
        }
    }
}
