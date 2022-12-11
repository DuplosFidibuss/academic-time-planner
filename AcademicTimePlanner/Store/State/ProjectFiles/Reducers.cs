using AcademicTimePlanner.DataMapping.Plan;
using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class Reducers
    {
        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, SetPlanProjectsAction action)
        {
            return new ProjectFilesState(ProjectFilesState.CreationStep.NotCreating, true, action.PlanProjects);
        }

        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, SwitchCreationStepAction action)
        {
            if (action.NextStep == ProjectFilesState.CreationStep.EnableTasks)
            {
                return new ProjectFilesState(action.NextStep, state.Loaded, state.PlanProjects, action.PlanProject, new PlanTask(Guid.NewGuid()));
            }
            return new ProjectFilesState(action.NextStep, state.Loaded, state.PlanProjects, action.PlanProject);
        }

        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, CreatePlanTaskAction action)
        {
            state.PlanProject!.AddPlanTask(action.PlanTask);
            return new ProjectFilesState(ProjectFilesState.CreationStep.EnableTasks, state.Loaded, state.PlanProjects, state.PlanProject, new PlanTask(Guid.NewGuid()));
        }

        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, AddSingleEntryAction action)
        {
            return new ProjectFilesState(ProjectFilesState.CreationStep.AddSingleEntry, state.Loaded, state.PlanProjects, state.PlanProject, new PlanEntry(Guid.NewGuid()));
        }

        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, AddRepetitionEntryAction action)
        {
            return new ProjectFilesState(ProjectFilesState.CreationStep.AddRepetitionEntry, state.Loaded, state.PlanProjects, state.PlanProject, new PlanEntryRepetition(Guid.NewGuid()));
        }

        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, DownloadPlanProjectAction action)
        {
            return new ProjectFilesState(state.Loaded, true, state.PlanProjects, action.PlanProject);
        }
    }
}
