using Fluxor;

namespace AcademicTimePlanner.Store.State.TogglSettings;

[FeatureState]
public class TogglSettingsState
{
    public string TogglApiKey { get; }
    public string TogglWorkspaceId { get; }

    private TogglSettingsState() { }
    
    public TogglSettingsState(string togglApiKey, string togglWorkspaceId)
    {
        TogglApiKey = togglApiKey;
        TogglWorkspaceId = togglWorkspaceId;
    }
}