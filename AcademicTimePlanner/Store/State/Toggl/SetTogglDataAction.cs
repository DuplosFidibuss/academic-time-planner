using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.Toggl;

public class SetTogglDataAction
{
    public ImmutableSortedSet<string> TogglProjectsNames { get; }
    public SetTogglDataAction(ImmutableSortedSet<string> togglProjectsNames)
    {
        TogglProjectsNames = togglProjectsNames;
    }
}