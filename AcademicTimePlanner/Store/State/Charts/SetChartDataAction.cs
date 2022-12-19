using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class SetChartDataAction
    {
        public Data.ProjectsData ChartData { get; set; }

        public SetChartDataAction(Data.ProjectsData chartData)
        {
            ChartData = chartData;
        }
    }
}
