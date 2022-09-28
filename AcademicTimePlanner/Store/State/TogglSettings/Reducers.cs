using Fluxor;

namespace AcademicTimePlanner.Store.State.TogglSettings;

public class Reducers
{
    [ReducerMethod]
    public TogglSettingsState Reduce(TogglSettingsState state, SetTogglSettingsAction action) =>
        new TogglSettingsState(action.TogglApiKey, action.TogglWorkspaceId);
    
    [ReducerMethod]
    public static TogglSettingsState Reduce(TogglSettingsState state, SetTogglSettingsStateAction action) => action.State;
}