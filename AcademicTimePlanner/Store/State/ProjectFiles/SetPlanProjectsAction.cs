using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class SetPlanProjectsAction
    {
        public List<string> PlanProjectsNames { get; }
        public SetPlanProjectsAction(List<string> planProjectsNames)
        {
            PlanProjectsNames = planProjectsNames;
        }
    }
}
