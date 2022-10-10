namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglEntrySum
    {

        public TogglEntrySum(DateOnly date, int duration, int togglId, int togglTaskId)
        {
            Id = Guid.NewGuid();
            Date = date;
            Duration = duration;
            TogglId = togglId;
            TogglTaskId = togglTaskId;
        }

        public Guid Id { get; }

        public DateOnly Date { get; }

        public int Duration { get; } 

        public int TogglId { get; }

        public int TogglTaskId { get; }
    }
}
