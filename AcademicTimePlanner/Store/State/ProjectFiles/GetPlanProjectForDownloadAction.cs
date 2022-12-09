namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class GetPlanProjectForDownloadAction
    {
        public Guid ProjectId { get; set; }

        public GetPlanProjectForDownloadAction(Guid projectId)
        {
            ProjectId = projectId;
        }
    }
}
