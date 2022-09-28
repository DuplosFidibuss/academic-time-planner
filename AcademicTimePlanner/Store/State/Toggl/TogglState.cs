using Fluxor;

namespace AcademicTimePlanner.Store.State.Toggl;

[FeatureState]
public class TogglState
{
    public bool Loaded { get; }

    public List<TogglEntrySum> TogglEntrySums { get; }

    private TogglState() { }

    public TogglState(bool loaded, List<TogglEntrySum> togglEntrySums)
    {
        Loaded = loaded;
        TogglEntrySums = togglEntrySums;
    }
}