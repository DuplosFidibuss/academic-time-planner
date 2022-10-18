using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.Services.DataManagerService;
using Fluxor;
using System.Text.Json;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class Effects
    {
        private readonly IDataManagerService _dataManagerService;

        public Effects(IDataManagerService dataManagerService)
        {
            _dataManagerService = dataManagerService;
        }

        [EffectMethod]
        public async Task HandleAsync(LoadPlanProjectsAction action, IDispatcher dispatcher)
        {
            var planProject = JsonSerializer.Deserialize<PlanProject>(action.PlanProjectsJson);
            await _dataManagerService.SetPlanProjects(new List<PlanProject>(){ planProject });
            dispatcher.Dispatch(new SetPlanProjectsAction(1));
        }
    }
}
