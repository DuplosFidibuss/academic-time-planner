using AcademicTimePlanner.Data.DisplayData;

namespace AcademicTimePlanner.Store.State.Toggl
{
    public class SetTogglDataAction
    {
        public List<TogglLoadOverviewData> LoadOverview { get; }

        public SetTogglDataAction(List<TogglLoadOverviewData> loadOverview)
        {
            LoadOverview = loadOverview;
        }
    }
}