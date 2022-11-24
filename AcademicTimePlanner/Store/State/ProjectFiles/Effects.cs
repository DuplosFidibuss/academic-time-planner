using AcademicTimePlanner.DataMapping.Plan;
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
            var projectNamesList = await _dataManagerService.GetPlanProjectNames();
            dispatcher.Dispatch(new SetPlanProjectsAction(projectNamesList));
        }

        [EffectMethod]
        public async Task HandleAsync(FinishPlanProjectCreationAction action, IDispatcher dispatcher)
        {
            await _dataManagerService.AddPlanProject(action.PlanProject);
            var projectNamesList = await _dataManagerService.GetPlanProjectNames();
            dispatcher.Dispatch(new SetPlanProjectsAction(projectNamesList));
        }

        [EffectMethod]
        public async Task HandleAsync(GetPlanProjectForDownloadAction action, IDispatcher dispatcher)
        {
            var planProject = await _dataManagerService.GetPlanProjectByName(action.ProjectName);
            dispatcher.Dispatch(new DownloadPlanProjectAction(planProject));
        }
    }
}
