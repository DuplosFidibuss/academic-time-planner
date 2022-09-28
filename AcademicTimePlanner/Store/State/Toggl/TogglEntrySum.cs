namespace AcademicTimePlanner.Store.State.Toggl;

public class TogglEntrySum
{
    public DateOnly Date { get; }
    public int Duration { get; }

    private TogglEntrySum() { }

    public TogglEntrySum(DateOnly date, int duration)
    {
        Date = date;
        Duration = duration;
    }
}
