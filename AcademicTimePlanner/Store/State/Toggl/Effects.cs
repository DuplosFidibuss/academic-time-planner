using AcademicTimePlanner.DataMapping.Toggl;
using AcademicTimePlanner.Services.DataManagerService;
using AcademicTimePlanner.Services.TogglService;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Toggl;

public class Effects
{
    private readonly ITogglService _togglService;
    private readonly IDataManagerService _dataManagerService;

    public Effects(ITogglService togglService, IDataManagerService dataManagerService)
    {
        _togglService = togglService;
        _dataManagerService = dataManagerService;
    }

    [EffectMethod]
    public async Task HandleAsync(FetchTogglDataAction action, IDispatcher dispatcher)
    {
        List<TogglProject> togglDetailResponseWithSinceDate = await _togglService.GetTogglProjects(DateOnly.FromDateTime(DateTime.Now).AddDays(-30));
        await _dataManagerService.SetTogglProjects(togglDetailResponseWithSinceDate);
        dispatcher.Dispatch(new SetTogglDataAction(togglDetailResponseWithSinceDate.Count));
    }
}