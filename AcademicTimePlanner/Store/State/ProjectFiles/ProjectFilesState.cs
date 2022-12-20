using AcademicTimePlanner.ApplicationData.Plan;
using Fluxor;

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
            EnterEntries,
            AddSingleEntry,
            AddRepetitionEntry,
            FinalOverview
        }

        public bool Loaded { get; }

        public CreationStep Step { get; }

        public List<PlanProject> PlanProjects { get; } = new List<PlanProject>();

        public PlanProject? PlanProject { get; }

        public PlanTask? PlanTask { get; }

        public PlanEntry? PlanEntry { get; }

        public PlanEntryRepetition? PlanEntryRepetition { get; }

        private ProjectFilesState() { }

        public ProjectFilesState(CreationStep step, bool loaded, List<PlanProject> planProjects)
        {
            Step = step;
            Loaded = loaded;
            PlanProjects = planProjects;
        }

        public ProjectFilesState(CreationStep step, bool loaded, List<PlanProject> planProjects, PlanProject planProject)
        {
            Step = step;
            Loaded = loaded;
            PlanProjects = planProjects;
            PlanProject = planProject;
        }

        public ProjectFilesState(CreationStep step, bool loaded, List<PlanProject> planProjects, PlanProject planProject, PlanTask planTask)
        {
            Step = step;
            Loaded = loaded;
            PlanProjects = planProjects;
            PlanProject = planProject;
            PlanTask = planTask;
        }

        public ProjectFilesState(CreationStep step, bool loaded, List<PlanProject> planProjects, PlanProject planProject, PlanEntry planEntry)
        {
            Step = step;
            Loaded = loaded;
            PlanProjects = planProjects;
            PlanProject = planProject;
            PlanEntry = planEntry;
        }
        public ProjectFilesState(CreationStep step, bool loaded, List<PlanProject> planProjects, PlanProject planProject, PlanEntryRepetition planEntryRepetition)
        {
            Step = step;
            Loaded = loaded;
            PlanProjects = planProjects;
            PlanProject = planProject;
            PlanEntryRepetition = planEntryRepetition;
        }
    }
}
