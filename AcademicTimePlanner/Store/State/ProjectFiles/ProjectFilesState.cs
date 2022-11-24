using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Plan;
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
            AddRepetitionEntry
        }

        public bool Loaded { get; }

        public bool IsDownloading { get; set; }

        public CreationStep Step { get; }

        public List<string> PlanProjectsNames { get; } = new List<string>();

        public PlanProject? PlanProject { get; }

        public PlanTask? PlanTask { get; }

        public PlanEntry? PlanEntry { get; }

        public PlanEntryRepetition? PlanEntryRepetition { get; }

        public PlanProjectDownloader PlanProjectDownloader { get; } = new PlanProjectDownloader();

        private ProjectFilesState() { }

        public ProjectFilesState(CreationStep step, bool loaded, List<string> planProjectsNames)
        {
            Step = step;
            Loaded = loaded;
            PlanProjectsNames = planProjectsNames;
        }
        public ProjectFilesState(CreationStep step, bool loaded, List<string> planProjectsNames, PlanProject planProject)
        {
            Step = step;
            Loaded = loaded;
            PlanProjectsNames = planProjectsNames;
            PlanProject = planProject;
        }

        public ProjectFilesState(CreationStep step, bool loaded, List<string> planProjectsNames, PlanProject planProject, PlanTask planTask)
        {
            Step = step;
            Loaded = loaded;
            PlanProjectsNames = planProjectsNames;
            PlanProject = planProject;
            PlanTask = planTask;
        }

        public ProjectFilesState(CreationStep step, bool loaded, List<string> planProjectsNames, PlanProject planProject, PlanEntry planEntry)
        {
            Step = step;
            Loaded = loaded;
            PlanProjectsNames = planProjectsNames;
            PlanProject = planProject;
            PlanEntry = planEntry;
        }
        public ProjectFilesState(CreationStep step, bool loaded, List<string> planProjectsNames, PlanProject planProject, PlanEntryRepetition planEntryRepetition)
        {
            Step = step;
            Loaded = loaded;
            PlanProjectsNames = planProjectsNames;
            PlanProject = planProject;
            PlanEntryRepetition = planEntryRepetition;
        }

        public ProjectFilesState(bool loaded, bool isDownloading, List<string> planProjectsNames, PlanProject planProject)
        {
            Loaded = loaded;
            IsDownloading = isDownloading;
            PlanProjectsNames = planProjectsNames;
            PlanProject = planProject;
        }
    }
}
