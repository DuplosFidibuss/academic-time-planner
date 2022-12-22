namespace AcademicTimePlanner.Data.ApplicationData.Toggl
{
    /// <summary>
    /// This class implements the Toggl entry sum.
    /// This sum consists of the duration sum of Toggl entries provided by Toggl Track.
    /// </summary>
    public class TogglEntrySum
    {
        public Guid Id { get; }

        public DateTime Date { get; }

        public double Duration { get; set; }

        public long TogglId { get; }

        public long TogglTaskId { get; }

        public TogglEntrySum(DateTime date, double duration, long togglId, long togglTaskId)
        {
            Id = Guid.NewGuid();
            Date = date;
            Duration = duration;
            TogglId = togglId;
            TogglTaskId = togglTaskId;
        }
    }
}
