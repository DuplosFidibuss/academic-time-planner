using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    [FeatureState]
    public class ChartsState
    {
        public bool Loaded { get; }

        public ChartData? ChartData { get; }

        public DateTime FilterStartDate { get; set; }

        public DateTime FilterEndDate { get; set; }

        private ChartsState() { }

        public ChartsState(bool loaded, ChartData chartData, DateTime filterStartDate, DateTime filterEndDate)
        {
            Loaded = loaded;
            ChartData = chartData;
            FilterStartDate = filterStartDate;
            FilterEndDate = filterEndDate;
        }
    }
}
