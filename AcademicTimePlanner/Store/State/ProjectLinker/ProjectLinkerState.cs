using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Plan;
using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    [FeatureState]
    public class ProjectLinkerState
    {
        public enum LinkingStep
        {
            ProjectLinking,
            TaskLinking
        }

        public bool Loaded { get; }

        public LinkingStep Step { get; }

        public DisplayData? ProjectsData { get; }

        public ProjectSelector ProjectSelector { get; } = new();

        public TaskSelector TaskSelector { get; } = new();

        public PlanProject PlanProject { get; }

        private ProjectLinkerState() { }

        public ProjectLinkerState(bool loaded, LinkingStep step, DisplayData projectsData)
        {
            Loaded = loaded;
            Step = step;
            ProjectsData = projectsData;
        }

        public ProjectLinkerState(bool loaded, LinkingStep step, DisplayData projectsData, PlanProject planProject)
        {
            Loaded = loaded;
            Step = step;
            ProjectsData = projectsData;
            PlanProject = planProject;
        }
    }
}
