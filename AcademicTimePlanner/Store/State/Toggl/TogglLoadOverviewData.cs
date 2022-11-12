namespace AcademicTimePlanner.Store.State.Toggl
{
    public class TogglLoadOverviewData
    {
        public string TogglProjectName { get; }

        public bool IsDeleted { get; }

        public string PlanProjectName { get; }

        public TogglLoadOverviewData(string togglProjectName, bool isDeleted, string planProjectName)
        {
            TogglProjectName = togglProjectName;
            IsDeleted = isDeleted;
            PlanProjectName = planProjectName;
        }
    }
}
