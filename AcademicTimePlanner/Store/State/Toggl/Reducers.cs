using Fluxor;

namespace AcademicTimePlanner.Store.State.Toggl;

public class Reducers
{
    [ReducerMethod]
    public static TogglState Reduce(TogglState state , SetTogglDataAction action)
    {
        return new TogglState(true, action.LoadOverview, DateTime.Now);
    }
}