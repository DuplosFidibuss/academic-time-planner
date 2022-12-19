using AcademicTimePlanner.Data;
using AcademicTimePlanner.UIModels;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    [FeatureState]
    public class ChartsState
    {
        public bool Loaded { get; set; }

        public bool LoadedFiltered { get; }

        public Data.ProjectsData? ChartData { get; }

        public DateFilter? DateFilter { get; }

        private ChartsState() { }

        public ChartsState(bool loaded, bool loadedFiltered, Data.ProjectsData chartData, DateFilter dateFilter)
        {
            Loaded = loaded;
            LoadedFiltered = loadedFiltered;
            ChartData = chartData;
            DateFilter = dateFilter;
        }
    }
}
