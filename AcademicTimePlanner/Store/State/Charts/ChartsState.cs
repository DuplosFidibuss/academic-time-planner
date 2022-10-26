using AcademicTimePlanner.Data;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    [FeatureState]
    public class ChartsState
    {
        public bool Loaded { get; }

        public ChartData? ChartData { get; }

        public DateTime DefaultStartDate { get; set; } = DateTime.Today.AddDays(-30);

        public DateTime DefaultEndDate { get; set; } = DateTime.Today;

        private ChartsState() { }

        public ChartsState(bool loaded, ChartData chartData)
        {
            Loaded = loaded;
            ChartData = chartData;
        }
    }
}
