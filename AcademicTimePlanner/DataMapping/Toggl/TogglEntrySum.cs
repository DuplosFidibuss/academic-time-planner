namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglEntrySum
    {
        public Guid Id { get; }

        public DateOnly Date { get; }

        public int Duration { get; set; }

        public long TogglId { get; }

        public long TogglTaskId { get; }

        /// <summary>
        /// This class implements the Toggl entry sum.
        /// This sum consists of Toggl entries provided by Toggl Track.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="duration"></param>
        /// <param name="togglId"></param>
        /// <param name="togglTaskId"></param>

        public TogglEntrySum(DateOnly date, int duration, long togglId, long togglTaskId)
        {
            Id = Guid.NewGuid();
            Date = date;
            Duration = duration;
            TogglId = togglId;
            TogglTaskId = togglTaskId;
        }
    }
}
