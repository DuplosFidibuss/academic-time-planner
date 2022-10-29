using Fluxor;
using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class Reducers
    {
        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state , SetChartDataAction action)
        {
            return new ChartsState(true, false, action.ChartData, new DateFilter());
        }

        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state, FilterChartDataAction action)
        {
            return new ChartsState(true, true, state.ChartData!, state.DateFilter!);
        }

        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state, ChangeFilterAction action)
        {
            return new ChartsState(true, false, state.ChartData!, state.DateFilter!);
        }
    }
}
