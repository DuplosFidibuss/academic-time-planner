using AcademicTimePlanner.Services.DataManagerService;
using AcademicTimePlanner.Services.TogglService;
using AcademicTimePlanner.UIModels;
using Blazored.LocalStorage;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Toggl;

public class Effects
{
    private readonly ITogglService _togglService;
    private readonly IDataManagerService _dataManagerService;
    private readonly ILocalStorageService _localStorageService;

    public Effects(ITogglService togglService, IDataManagerService dataManagerService, ILocalStorageService localStorageService)
    {
        _togglService = togglService;
        _dataManagerService = dataManagerService;
        _localStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task HandleAsync(FetchTogglDataAction action, IDispatcher dispatcher)
    {
        var togglProjects = await _togglService.GetTogglProjects(DateOnly.FromDateTime(DateTime.Now).AddYears(-1));
        await _dataManagerService.UpdateTogglProjects(togglProjects);
        var togglLoadOverview = await _dataManagerService.GetTogglLoadOverview();
        dispatcher.Dispatch(new SetTogglDataAction(togglLoadOverview));
    }

    [EffectMethod]
    public async Task HandleAsync(SaveTogglSettingsAction action, IDispatcher dispatcher)
    {
        await _localStorageService.SetItemAsync(nameof(TogglSettings), action.TogglSettings);
        dispatcher.Dispatch(new FetchTogglDataAction());
    }
}