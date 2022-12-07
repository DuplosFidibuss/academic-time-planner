using AcademicTimePlanner.Services.DataManagerService;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Charts
{
    public class Effects
    {
        private readonly IDataManagerService _dataManagerService;

        public Effects(IDataManagerService dataManagerService)
        {
            _dataManagerService = dataManagerService;
        }

        [EffectMethod]
        public async Task HandleAsync(FetchChartDataAction action, IDispatcher dispatcher)
        {
            var chartData = await _dataManagerService.GetDisplayData();
            dispatcher.Dispatch(new SetChartDataAction(chartData));
        }
    }
}
