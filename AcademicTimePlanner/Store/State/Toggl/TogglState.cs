using Fluxor;

namespace AcademicTimePlanner.Store.State.Toggl;

[FeatureState]
public class TogglState
{
    public bool Loaded { get; }

    public int NumberOfTogglProjects { get; }

    private TogglState() { }

    public TogglState(bool loaded, int numberOfTogglProjects)
    {
        Loaded = loaded;
        NumberOfTogglProjects = numberOfTogglProjects;
    }
}