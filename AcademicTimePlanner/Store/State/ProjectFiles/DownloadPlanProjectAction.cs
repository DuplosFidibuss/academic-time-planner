using AcademicTimePlanner.DataMapping.Plan;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class DownloadPlanProjectAction
    {
        public PlanProject PlanProject { get; set; }

        public DownloadPlanProjectAction(PlanProject planProject)
        {
            PlanProject = planProject;
        }
    }
}
