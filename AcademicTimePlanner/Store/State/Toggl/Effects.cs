using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Toggl;
using AcademicTimePlanner.Services.DataManagerService;
using AcademicTimePlanner.Services.TogglService;
using Blazored.LocalStorage;
using Fluxor;
using System.Collections.Immutable;

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
        var updatedTogglProjects = await _togglService.GetTogglProjects(DateOnly.FromDateTime(DateTime.Now).AddYears(-1));
        var currentTogglProjects = await _dataManagerService.GetTogglProjects();

        var deletedTogglProjects = new List<TogglProject>();
        if (currentTogglProjects.Count > 0)
            deletedTogglProjects.AddRange(currentTogglProjects.FindAll(project => !updatedTogglProjects.Any(updatedProject => project.TogglId == updatedProject.TogglId)));

        var allTogglProjects = updatedTogglProjects.Union(deletedTogglProjects).ToList();
        await _dataManagerService.SetTogglProjects(allTogglProjects);

        var planProjects = await _dataManagerService.GetPlanProjects();
        var loadOverview = new List<TogglLoadOverviewData>();
        allTogglProjects.ForEach(togglProject => loadOverview.Add(new TogglLoadOverviewData(togglProject.Name, !deletedTogglProjects.Contains(togglProject), planProjects.Find(planProject => planProject.TogglProjectId == togglProject.TogglId).Name)));
        dispatcher.Dispatch(new SetTogglDataAction(loadOverview));
    }

	[EffectMethod]
	public async Task HandleAsync(SaveTogglSettingsAction action, IDispatcher dispatcher)
	{
		await _localStorageService.SetItemAsync(nameof(TogglSettings), action.TogglSettings);
		dispatcher.Dispatch(new FetchTogglDataAction());
	}
}