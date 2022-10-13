namespace AcademicTimePlanner.Store.State.Toggl;

public class SetTogglDataAction
{
    public int NumberOfTogglProjects { get; }
    public SetTogglDataAction(int numberOfTogglProjects)
    {
        NumberOfTogglProjects = numberOfTogglProjects;
    }
}