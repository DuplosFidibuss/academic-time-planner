using AcademicTimePlanner.Store.State.TogglSettings;
using Blazored.LocalStorage;
using Fluxor;

namespace AcademicTimePlanner.Store.Middleware.Persistence;

public class Effects
{
    private readonly ILocalStorageService _localStorage;
    private readonly IState<TogglSettingsState> _togglSettingsState;

    public Effects(IState<TogglSettingsState> togglSettingsState, ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _togglSettingsState = togglSettingsState;
    }

    [EffectMethod]
    public async Task HandleAsync(PersistStateAction action, IDispatcher dispatcher)
    {
        await _localStorage.SetItemAsync(nameof(TogglSettingsState), _togglSettingsState.Value);
    }

    [EffectMethod]
    public async Task HandleAsync(StoreInitializedAction action, IDispatcher dispatcher)
    {
        var togglSettingsState = await _localStorage.GetItemAsync<TogglSettingsState>(nameof(TogglSettingsState));
        if (togglSettingsState != null)
        {
            dispatcher.Dispatch(new SetTogglSettingsStateAction(togglSettingsState));
        }
    }
}