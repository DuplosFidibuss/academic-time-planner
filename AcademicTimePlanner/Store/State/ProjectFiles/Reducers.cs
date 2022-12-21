using AcademicTimePlanner.ApplicationData.Plan;
using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class Reducers
    {
        /// <summary>
        /// Returns new <see cref="ProjectFilesState"/> with newly set PlanProjects provided by the action.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, SetPlanProjectsAction action)
        {
            return new ProjectFilesState(ProjectFilesState.CreationStep.NotCreating, true, action.PlanProjects);
        }

        /// <summary>
        /// Returns new <see cref="ProjectFilesState"/> with newly set CreationStep provided by the action.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, SwitchCreationStepAction action)
        {
            if (action.NextStep == ProjectFilesState.CreationStep.EnableTasks)
            {
                return new ProjectFilesState(action.NextStep, state.Loaded, state.PlanProjects, action.PlanProject, new PlanTask(Guid.NewGuid()));
            }
            return new ProjectFilesState(action.NextStep, state.Loaded, state.PlanProjects, action.PlanProject);
        }

        /// <summary>
        /// Adds the <see cref="PlanTask"/> provided by the action to the <see cref="PlanProject"/> of the
        /// current <see cref="ProjectFilesState"/> and returns new <see cref="ProjectFilesState"/> with
        /// the updated PlanProject and a new PlanTask.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, CreatePlanTaskAction action)
        {
            state.PlanProject!.AddPlanTask(action.PlanTask);
            return new ProjectFilesState(ProjectFilesState.CreationStep.EnableTasks, state.Loaded, state.PlanProjects, state.PlanProject, new PlanTask(Guid.NewGuid()));
        }

        /// <summary>
        /// Adds the <see cref="PlanEntry"/> provided by the action to the <see cref="PlanProject"/> of the
        /// current <see cref="ProjectFilesState"/> and returns new <see cref="ProjectFilesState"/> with
        /// the updated PlanProject and a new PlanEntry.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, AddSingleEntryAction action)
        {
            return new ProjectFilesState(ProjectFilesState.CreationStep.AddSingleEntry, state.Loaded, state.PlanProjects, state.PlanProject, new PlanEntry(Guid.NewGuid()));
        }

        /// <summary>
        /// Adds the <see cref="PlanEntryRepetition"/> provided by the action to the <see cref="PlanProject"/> of the
        /// current <see cref="ProjectFilesState"/> and returns new <see cref="ProjectFilesState"/> with
        /// the updated PlanProject and a new PlanEntryRepetition.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, AddRepetitionEntryAction action)
        {
            return new ProjectFilesState(ProjectFilesState.CreationStep.AddRepetitionEntry, state.Loaded, state.PlanProjects, state.PlanProject, new PlanEntryRepetition(Guid.NewGuid()));
        }
    }
}
