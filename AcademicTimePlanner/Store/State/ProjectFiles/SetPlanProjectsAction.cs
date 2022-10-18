namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class SetPlanProjectsAction
    {
        public int NumberOfPlanProjects { get; }
        public SetPlanProjectsAction(int numberOfPlanProjects)
        {
            NumberOfPlanProjects = numberOfPlanProjects;
        }
    }
}
