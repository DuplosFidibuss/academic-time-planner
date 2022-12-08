namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class DeletePlanProjectAction
    {
        public string ProjectName { get; set; }

        public DeletePlanProjectAction(string projectName)
        {
            ProjectName = projectName;
        }
    }
}
