namespace AcademicTimePlanner.DisplayData
{
    public class TogglLoadOverviewData
    {
        public string TogglProjectName { get; }

        public bool IsDeleted { get; }

        public string PlanProjectNames { get; }

        public TogglLoadOverviewData(string togglProjectName, bool isDeleted, string planProjectNames)
        {
            TogglProjectName = togglProjectName;
            IsDeleted = isDeleted;
            PlanProjectNames = planProjectNames;
        }
    }
}
