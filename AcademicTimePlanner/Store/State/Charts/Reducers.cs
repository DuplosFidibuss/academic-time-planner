using AcademicTimePlanner.UIModels;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class Reducers
    {
        /// <summary>
        /// Returns new <see cref="ChartsState"/> with newly set chart data provided by the action.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state, SetChartDataAction action)
        {
            return new ChartsState(true, false, action.ChartData, new DateFilter());
        }

        /// <summary>
        /// Returns new <see cref="ChartsState"/> with newly set filter.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state, FilterChartDataAction action)
        {
            return new ChartsState(true, true, state.ChartData!, state.DateFilter!);
        }

        /// <summary>
        /// Returns new <see cref="ChartsState"/> indicating that filter is to be reset.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state, ChangeFilterAction action)
        {
            return new ChartsState(true, false, state.ChartData!, state.DateFilter!);
        }
    }
}
