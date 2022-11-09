using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.Toggl;

public class SetTogglDataAction
{
    public ImmutableSortedDictionary<string, bool> TogglProjectsNames { get; }
    public SetTogglDataAction(ImmutableSortedDictionary<string, bool> togglProjectsNames)
    {
        TogglProjectsNames = togglProjectsNames;
    }
}