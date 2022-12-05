using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class Reducers
    {
        [ReducerMethod]
        public static ProjectLinkerState Reduce(ProjectLinkerState state, SetChartDataAction action)
        {
            return new ProjectLinkerState(true, action.ChartData);
        }
    }
}
