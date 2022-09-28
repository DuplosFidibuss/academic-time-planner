using AcademicTimePlanner.Store.State.Toggl;
using Fluxor;

namespace AcademicTimePlanner.Store.State.TogglSettings;

public class Effects
{
    private IState<TogglSettingsState> _togglSettingsState;
    
    public Effects(IConfiguration configuration, IState<TogglSettingsState> togglSettingsState)
    {
        _togglSettingsState = togglSettingsState;
    }

    [EffectMethod]
    public async Task HandleAsync(SetTogglSettingsAction action, IDispatcher dispatcher)
    {
        if (!string.IsNullOrEmpty(_togglSettingsState.Value.TogglApiKey) && !string.IsNullOrEmpty(_togglSettingsState.Value.TogglWorkspaceId))
        {
            dispatcher.Dispatch(new FetchTogglDataAction());
        }
    }
    
    [EffectMethod]
    public async Task HandleAsync(SetTogglSettingsStateAction action, IDispatcher dispatcher)
    {
        if (!string.IsNullOrEmpty(_togglSettingsState.Value.TogglApiKey) && !string.IsNullOrEmpty(_togglSettingsState.Value.TogglWorkspaceId))
        {
            dispatcher.Dispatch(new FetchTogglDataAction());
        }
    }
}