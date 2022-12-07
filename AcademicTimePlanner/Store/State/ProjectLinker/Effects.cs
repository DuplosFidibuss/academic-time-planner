using AcademicTimePlanner.Services.DataManagerService;
using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class Effects
    {
        private readonly IDataManagerService _dataManagerService;

        public Effects(IDataManagerService dataManagerService)
        {
            _dataManagerService = dataManagerService;
        }

        [EffectMethod]
        public async Task HandleAsync(FetchProjectsDataAction action, IDispatcher dispatcher)
        {
            var chartData = await _dataManagerService.GetChartData();
            dispatcher.Dispatch(new SetProjectsDataAction(chartData));
        }
    }
}
