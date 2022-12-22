using AcademicTimePlanner.Data.ApplicationData.Plan;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class SetPlanProjectsAction
    {
        public List<PlanProject> PlanProjects { get; }

        public SetPlanProjectsAction(List<PlanProject> planProjects)
        {
            PlanProjects = planProjects;
        }
    }
}
