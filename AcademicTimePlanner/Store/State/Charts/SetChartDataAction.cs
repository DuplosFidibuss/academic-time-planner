using AcademicTimePlanner.DisplayData;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class SetChartDataAction
    {
        public ProjectsData ChartData { get; set; }

        public SetChartDataAction(ProjectsData chartData)
        {
            ChartData = chartData;
        }
    }
}
