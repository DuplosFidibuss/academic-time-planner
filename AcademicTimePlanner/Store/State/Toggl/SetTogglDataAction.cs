using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Store.State.Toggl;

public class SetTogglDataAction
{
    public List<TogglEntrySum> TogglEntrySums { get; }
    public SetTogglDataAction(List<TogglEntrySum> togglEntrySums)
    {
        TogglEntrySums = togglEntrySums;
    }
}