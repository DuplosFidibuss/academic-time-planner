using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class SetChartDataAction
    {
        public Data.DisplayData ChartData { get; set; }

        public SetChartDataAction(Data.DisplayData chartData)
        {
            ChartData = chartData;
        }
    }
}
