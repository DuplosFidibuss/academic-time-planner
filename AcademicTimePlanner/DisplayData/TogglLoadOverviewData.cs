namespace AcademicTimePlanner.DisplayData
{
    /// <summary>
    /// Holds state information about a TogglProject.
    /// This information is used for the status display on the Toggl page.
    /// </summary>
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
