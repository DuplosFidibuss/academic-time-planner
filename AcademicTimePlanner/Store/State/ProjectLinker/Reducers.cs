using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class Reducers
    {
        /// <summary>
        /// Returns new <see cref="ProjectLinkerState"/> with newly set projects data provided by the action.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ProjectLinkerState Reduce(ProjectLinkerState state, SetProjectsDataAction action)
        {
            return new ProjectLinkerState(true, state.Step, action.ProjectsData, state.PlanProject);
        }

        /// <summary>
        /// Returns new <see cref="ProjectLinkerState"/> with newly set LinkingStep provided by the action.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ReducerMethod]
        public static ProjectLinkerState Reduce(ProjectLinkerState state, SwitchLinkingStepAction action)
        {
            return new ProjectLinkerState(true, action.Step, state.ProjectsData!, action.PlanProject);
        }
    }
}
