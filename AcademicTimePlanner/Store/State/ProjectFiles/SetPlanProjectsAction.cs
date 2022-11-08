using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class SetPlanProjectsAction
    {
        public ImmutableSortedSet<string> PlanProjectsNames { get; }
        public SetPlanProjectsAction(ImmutableSortedSet<string> planProjectsNames)
        {
            PlanProjectsNames = planProjectsNames;
        }
    }
}
