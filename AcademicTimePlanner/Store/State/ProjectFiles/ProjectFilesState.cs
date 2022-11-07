using Fluxor;
using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    [FeatureState]
    public class ProjectFilesState
    {
        public bool Loaded { get; }

        public ImmutableSortedSet<string> PlanProjectsNames { get; }

        private ProjectFilesState() { }

        public ProjectFilesState(bool loaded, ImmutableSortedSet<string> planProjectsNames)
        {
            Loaded = loaded;
            PlanProjectsNames = planProjectsNames;
        }
    }
}
