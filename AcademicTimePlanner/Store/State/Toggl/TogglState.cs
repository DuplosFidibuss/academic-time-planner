using Fluxor;
using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.Toggl;

[FeatureState]
public class TogglState
{
    public bool Loaded { get; }

    public ImmutableSortedDictionary<string, bool> TogglProjectsNames { get; }

    public DateTime LastSynchronized { get; }

    private TogglState() { }

    public TogglState(bool loaded, ImmutableSortedDictionary<string, bool> togglProjectsNames, DateTime lastSynchronized)
    {
        Loaded = loaded;
        TogglProjectsNames = togglProjectsNames;
		LastSynchronized = lastSynchronized;
    }
}