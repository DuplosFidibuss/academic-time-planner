using AcademicTimePlanner.ApplicationData.Plan;
using AcademicTimePlanner.Services.DataManagerService;
using Fluxor;
using Newtonsoft.Json;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class Effects
    {
        private readonly IDataManagerService _dataManagerService;

        public Effects(IDataManagerService dataManagerService)
        {
            _dataManagerService = dataManagerService;
        }

        /// <summary>
        /// Fetches plan projects stored in the <see cref="DataManagement.DataManager"/>.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
        public async Task HandleAsync(FetchPlanProjectsAction action, IDispatcher dispatcher)
        {
            var planProjects = await _dataManagerService.GetPlanProjects();
            dispatcher.Dispatch(new SetPlanProjectsAction(planProjects));
        }

        /// <summary>
        /// Loads plan projects from the JSON strings provided by the action and stores
        /// them in the <see cref="DataManagement.DataManager"/>.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
        public async Task HandleAsync(LoadPlanProjectsAction action, IDispatcher dispatcher)
        {
            var planProjects = new List<PlanProject>();
            foreach (var project in action.PlanProjectsJson)
            {
                planProjects.Add(JsonConvert.DeserializeObject<PlanProject>(project, new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                })!);
            }
            await _dataManagerService.UpdatePlanProjects(planProjects);
            var updatedPlanProjects = await _dataManagerService.GetPlanProjects();
            dispatcher.Dispatch(new SetPlanProjectsAction(updatedPlanProjects));
        }

        /// <summary>
        /// Updates the <see cref="DataManagement.DataManager"/> with the <see cref="PlanProject"/>
        /// provided by the action.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
        public async Task HandleAsync(FinishPlanProjectCreationAction action, IDispatcher dispatcher)
        {
            await _dataManagerService.UpdatePlanProject(action.PlanProject);
            var planProjects = await _dataManagerService.GetPlanProjects();
            dispatcher.Dispatch(new SetPlanProjectsAction(planProjects));
        }

        /// <summary>
        /// Deletes the <see cref="PlanProject"/> provided by the action from the PlanProjects
        /// stored in the <see cref="DataManagement.DataManager"/>.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
        public async Task HandleAsync(DeletePlanProjectAction action, IDispatcher dispatcher)
        {
            await _dataManagerService.DeletePlanProject(action.ProjectId);
            var planProjects = await _dataManagerService.GetPlanProjects();
            dispatcher.Dispatch(new SetPlanProjectsAction(planProjects));
        }
    }
}
