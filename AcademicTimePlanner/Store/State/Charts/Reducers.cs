using Fluxor;
using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class Reducers
    {
        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state , SetChartDataAction action)
        {
            return new ChartsState(true, action.ChartData, new DateFilter(DateTime.Today.AddDays(-30), DateTime.Today));
        }

        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state, FilterChartDataAction action)
        {
            return new ChartsState(true, state.ChartData!, state.DateFilter!);
        }
    }
}
