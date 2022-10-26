﻿using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class Reducers
    {
        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state , SetChartDataAction action)
        {
            return new ChartsState(true, action.ChartData);
        }

        [ReducerMethod]
        public static ChartsState Reduce(ChartsState state, FilterChartDataAction action)
        {
            return new ChartsState(true, state.ChartData!);
        }
    }
}
