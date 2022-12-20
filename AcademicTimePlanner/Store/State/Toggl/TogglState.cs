using AcademicTimePlanner.DisplayData;
using Fluxor;
using System.Collections.Immutable;

namespace AcademicTimePlanner.Store.State.Toggl;

[FeatureState]
public class TogglState
{
    public bool Loaded { get; }

    public List<TogglLoadOverviewData> LoadOverview { get; }

    public DateTime LastSynchronized { get; }

    private TogglState() { }

    public TogglState(bool loaded, List<TogglLoadOverviewData> loadOverview, DateTime lastSynchronized)
    {
        Loaded = loaded;
        LoadOverview= loadOverview;
		LastSynchronized = lastSynchronized;
    }
}