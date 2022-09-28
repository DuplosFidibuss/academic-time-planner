using Fluxor;

namespace AcademicTimePlanner.Store.State.Wrapper;

public class Reducers
{
    [ReducerMethod]
    public static WrapperState Reduce(WrapperState state, SetTitleAction action) =>
        new WrapperState(action.Title);
}