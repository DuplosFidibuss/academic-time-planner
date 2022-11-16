using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class Reducers
    {
        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, SetPlanProjectsAction action)
        {
            return new ProjectFilesState(ProjectFilesState.CreationStep.NotCreating, true, action.PlanProjectsNames);
        }

        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state, SwitchCreationStepAction action)
        {
            return new ProjectFilesState(action.NextStep, state.Loaded, state.PlanProjectsNames, action.PlanProject);   
        }
    }
}
