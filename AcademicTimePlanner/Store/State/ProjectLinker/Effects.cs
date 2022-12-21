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

        /// <summary>
        /// Fetches the project data stored in the <see cref="DataManagement.DataManager"/>.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
        public async Task HandleAsync(FetchProjectsDataAction action, IDispatcher dispatcher)
        {
            var projectsData = await _dataManagerService.GetProjectsData();
            dispatcher.Dispatch(new SetProjectsDataAction(projectsData));
        }

        /// <summary>
        /// Updates the project data stored in the <see cref="DataManagement.DataManager"/>
        /// with the project data provided by the action.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
        public async Task HandleAsync(SaveProjectsDataAction action, IDispatcher dispatcher)
        {
            await _dataManagerService.UpdatePlanProjects(action.ProjectsData.PlanProjects);
            var projectsData = await _dataManagerService.GetProjectsData();
            dispatcher.Dispatch(new SetProjectsDataAction(projectsData));
        }
    }
}
