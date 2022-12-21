using Fluxor;

namespace AcademicTimePlanner.Store.State.Toggl
{
    public class Reducers
    {
        /// <summary>
        /// Returns new <see cref="TogglState"/> with overview of loaded Toggl data
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static TogglState Reduce(TogglState state, SetTogglDataAction action)
        {
            return new TogglState(true, action.LoadOverview, DateTime.Now);
        }
    }
}