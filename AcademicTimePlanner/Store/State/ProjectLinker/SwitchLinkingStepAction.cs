using AcademicTimePlanner.ApplicationData.Plan;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class SwitchLinkingStepAction
    {
        public ProjectLinkerState.LinkingStep Step { get; }

        public PlanProject PlanProject { get; }

        public SwitchLinkingStepAction(ProjectLinkerState.LinkingStep step, PlanProject planProject)
        {
            Step = step;
            PlanProject = planProject;
        }
    }
}
