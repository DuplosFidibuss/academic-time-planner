namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class DeletePlanProjectAction
    {
        public Guid ProjectId { get; set; }

        public DeletePlanProjectAction(Guid projectId)
        {
            ProjectId = projectId;
        }
    }
}
