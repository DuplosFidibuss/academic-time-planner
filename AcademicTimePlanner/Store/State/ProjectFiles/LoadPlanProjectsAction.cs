namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class LoadPlanProjectsAction
    {
        public List<string> PlanProjectsJson { get; set; }

        public LoadPlanProjectsAction(List<string> planProjectsJson)
        {
            PlanProjectsJson = planProjectsJson;
        }
    }
}
