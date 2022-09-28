using AcademicTimePlanner.Services.TogglApiService;
using AcademicTimePlanner.Store.State.Toggl;

namespace AcademicTimePlanner.Services.TogglService;

public class TogglService : ITogglService
{
    private ITogglApiService _togglApiService;

    public TogglService(ITogglApiService togglApiService)
    {
        _togglApiService = togglApiService;
    }
    
    public async Task<List<TogglEntrySum>> GetTogglEntrySumAsync(DateOnly since)
    {
        TogglDetailResponse togglDetailResponseWithSinceDate = await _togglApiService.GetDetailsSinceAsync(since);

        var togglDetailResponseDatasPerDates = togglDetailResponseWithSinceDate.Data.GroupBy(entry => entry.DateTime.Date, entry => entry, (date, group) => (GroupDate:date, Datas:group.ToList()));

        var togglEntrySumList = togglDetailResponseDatasPerDates.Select(togglDetailResponseDatasPerDate =>
        {
            var dateOnly = DateOnly.FromDateTime(togglDetailResponseDatasPerDate.GroupDate);
            var duration = togglDetailResponseDatasPerDate.Datas.Sum(data => data.Duration);
            return new TogglEntrySum(dateOnly, duration);
        }).ToList();

        return togglEntrySumList;
    }
}