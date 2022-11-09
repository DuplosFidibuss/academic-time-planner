using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.Services.DataManagerService;
using Fluxor;
using System.Collections.Immutable;
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
            var planProjects = new List<PlanProject>();
            foreach (var project in action.PlanProjectsJson)
            {
                planProjects.Add(JsonSerializer.Deserialize<PlanProject>(project)!);
            }
            await _dataManagerService.SetPlanProjects(planProjects);
            var projectNamesList = (from planProject in planProjects select planProject.Name).ToImmutableSortedSet();
            dispatcher.Dispatch(new SetPlanProjectsAction(projectNamesList));
        }
    }
}
