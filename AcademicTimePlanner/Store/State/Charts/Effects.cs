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

        /// <summary>
        /// Fetches projects data stored in the <see cref="DataManagement.DataManager"/>.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
        public async Task HandleAsync(FetchChartDataAction action, IDispatcher dispatcher)
        {
            var chartData = await _dataManagerService.GetProjectsData();
            dispatcher.Dispatch(new SetChartDataAction(chartData));
        }
    }
}
