using AcademicTimePlanner.Data;
using AcademicTimePlanner.Services.DataManagerService;
using AcademicTimePlanner.Services.TogglService;
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

        var planProjects = await _dataManagerService.GetTogglLoadOverview();
        var loadOverview = new List<TogglLoadOverviewData>();
        allTogglProjects.ForEach(togglProject =>
        {
            var planProject = planProjects.Find(planProject => planProject.TogglProjectId == togglProject.TogglId);
            loadOverview.Add(new TogglLoadOverviewData(togglProject.Name != null ? togglProject.Name : "Entries without project", deletedTogglProjects.Contains(togglProject), planProject != null ? planProject.Name : "Not associated"));
        });
        dispatcher.Dispatch(new SetTogglDataAction(loadOverview));
    }

    [EffectMethod]
    public async Task HandleAsync(SaveTogglSettingsAction action, IDispatcher dispatcher)
    {
        await _localStorageService.SetItemAsync(nameof(TogglSettings), action.TogglSettings);
        dispatcher.Dispatch(new FetchTogglDataAction());
    }
}