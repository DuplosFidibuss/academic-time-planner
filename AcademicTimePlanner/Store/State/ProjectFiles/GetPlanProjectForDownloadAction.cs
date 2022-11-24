namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class GetPlanProjectForDownloadAction
    {
        public string ProjectName { get; set; }

        public GetPlanProjectForDownloadAction(string projectName)
        {
            ProjectName = projectName;
        }
    }
}
