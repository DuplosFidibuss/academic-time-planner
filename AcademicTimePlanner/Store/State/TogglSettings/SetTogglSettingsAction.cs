namespace AcademicTimePlanner.Store.State.TogglSettings;

public class SetTogglSettingsAction
{
    public string TogglApiKey { get; }
    public string TogglWorkspaceId { get; }

    public SetTogglSettingsAction(string togglApiKey, string togglWorkspaceId)
    {
        TogglApiKey = togglApiKey;
        TogglWorkspaceId = togglWorkspaceId;
    }
}