using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.UIModels;
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

        public ProjectsData? ProjectsData { get; }

        public ProjectSelector ProjectSelector { get; } = new();

        public TaskSelector TaskSelector { get; } = new();

        public PlanProject PlanProject { get; }

        private ProjectLinkerState() { }

        public ProjectLinkerState(bool loaded, LinkingStep step, ProjectsData projectsData, PlanProject planProject)
        {
            Loaded = loaded;
            Step = step;
            ProjectsData = projectsData;
            PlanProject = planProject;
        }
    }
}
