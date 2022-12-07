using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class Reducers
    {
        [ReducerMethod]
        public static ProjectLinkerState Reduce(ProjectLinkerState state, SetProjectsDataAction action)
        {
            return new ProjectLinkerState(true, action.ProjectsData);
        }
    }
}
