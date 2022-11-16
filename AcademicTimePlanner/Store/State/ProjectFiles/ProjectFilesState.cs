using Fluxor;
using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    [FeatureState]
    public class ProjectFilesState
    {
        public enum CreationStep
        {
            NotCreating, 
            NamingProject,
            EnableTasks,
            EnterEntries
        }

        public bool Loaded { get; }

        public CreationStep Step { get; }

        public ImmutableSortedSet<string> PlanProjectsNames { get; }

        private ProjectFilesState() { }

        public ProjectFilesState(CreationStep step, bool loaded, ImmutableSortedSet<string> planProjectsNames)
        {
            Step = step;
            Loaded = loaded;
            PlanProjectsNames = planProjectsNames;
        }
    }
}
