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
            var projectsData = await _dataManagerService.GetProjectsData();
            dispatcher.Dispatch(new SetProjectsDataAction(projectsData));
        }

        [EffectMethod]
        public async Task HandleAsync(SaveProjectsDataAction action, IDispatcher dispatcher)
        {
            await _dataManagerService.UpdatePlanProjects(action.ProjectsData.PlanProjects);
            var projectsData = await _dataManagerService.GetProjectsData();
            dispatcher.Dispatch(new SetProjectsDataAction(projectsData));
        }
    }
}
