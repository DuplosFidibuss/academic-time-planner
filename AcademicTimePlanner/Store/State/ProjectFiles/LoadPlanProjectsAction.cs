namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class LoadPlanProjectsAction
    {
        public string PlanProjectsJson { get; set; }

        public LoadPlanProjectsAction(string planProjectsJson)
        {
            PlanProjectsJson = planProjectsJson;
        }
    }
}
