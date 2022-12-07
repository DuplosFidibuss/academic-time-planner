using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class SetChartDataAction
    {
        public Data.Data ChartData { get; set; }

        public SetChartDataAction(Data.Data chartData)
        {
            ChartData = chartData;
        }
    }
}
