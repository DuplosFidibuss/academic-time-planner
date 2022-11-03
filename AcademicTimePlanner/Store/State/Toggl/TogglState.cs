using Fluxor;

namespace AcademicTimePlanner.Store.State.Toggl;

[FeatureState]
public class TogglState
{
    public bool Loaded { get; }

    public int NumberOfTogglProjects { get; }

	public DateTime LastSynchronized { get; }

    private TogglState() { }

    public TogglState(bool loaded, int numberOfTogglProjects, DateTime lastSynchronized)
    {
        Loaded = loaded;
        NumberOfTogglProjects = numberOfTogglProjects;
		LastSynchronized = lastSynchronized;
    }
}