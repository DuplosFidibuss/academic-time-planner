namespace AcademicTimePlanner.Store.State.TogglSettings;

public class SetTogglSettingsStateAction
{
    public TogglSettingsState State { get; }

    public SetTogglSettingsStateAction(TogglSettingsState state)
    {
        State = state;
    }
}