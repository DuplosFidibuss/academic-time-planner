using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class SetChartDataAction
    {
        public ChartData ChartData { get; set; }

        public SetChartDataAction(ChartData chartData)
        {
            ChartData = chartData;
        }
    }
}
