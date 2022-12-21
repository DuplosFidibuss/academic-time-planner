using Fluxor;

namespace AcademicTimePlanner.Store.State.Wrapper
{
    public class Reducers
    {
        /// <summary>
        /// Returns new <see cref="WrapperState"/> with newly set title provided by the action.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static WrapperState Reduce(WrapperState state, SetTitleAction action) =>
            new WrapperState(action.Title);
    }
}