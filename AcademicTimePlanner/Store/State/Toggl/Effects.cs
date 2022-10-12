using AcademicTimePlanner.DataMapping.Toggl;
using AcademicTimePlanner.Services.TogglService;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Toggl;

public class Effects
{
    private readonly ITogglService _togglService;

    public Effects(ITogglService togglService)
    {
        _togglService = togglService;
    }

    [EffectMethod]
    public async Task HandleAsync(FetchTogglDataAction action, IDispatcher dispatcher)
    {
        List<TogglEntrySum> togglDetailResponseWithSinceDate = await _togglService.GetTogglEntrySumAsync(DateOnly.FromDateTime(DateTime.Now).AddDays(-30));

        dispatcher.Dispatch(new SetTogglDataAction(togglDetailResponseWithSinceDate));
    }
}