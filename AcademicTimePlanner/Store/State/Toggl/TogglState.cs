using Fluxor;
using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.Toggl;

[FeatureState]
public class TogglState
{
    public bool Loaded { get; }

    public ImmutableSortedSet<string> TogglProjectsNames { get; }

    public DateTime LastSynchronized { get; }

    private TogglState() { }

    public TogglState(bool loaded, ImmutableSortedSet<string> togglProjectsNames, DateTime lastSynchronized)
    {
        Loaded = loaded;
        TogglProjectsNames = togglProjectsNames;
		LastSynchronized = lastSynchronized;
    }
}