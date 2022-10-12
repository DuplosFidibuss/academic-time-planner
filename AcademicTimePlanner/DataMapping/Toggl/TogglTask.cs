namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglTask
    {
        public const long NoTogglTaskId = -1;

        private List<TogglEntrySum> _togglEntrySums;

        /// <summary>
        /// This class implements the conection between ToggleTrack tasks and the application.
        /// Only application relevant data is saved.
        /// A Task can have multiple <see cref="TogglEntrySum"> Toggl entry sums</see>.
        /// </summary>
        /// <param name="togglId"></param>
        /// <param name="name"></param>
        public TogglTask(long togglId, string name)
        {
            Id = Guid.NewGuid();
            TogglId = togglId;
            Name = name;
            _togglEntrySums = new List<TogglEntrySum>();
        }

        public Guid Id { get; }

        public long TogglId { get; }

        public string Name { get; set; }

        public void AddEntry (TogglEntrySum entry)
        {
            var entrySumOfSameDay = _togglEntrySums.FindLast(entrySum => entrySum.Date.Equals(entry.Date));
            if (entrySumOfSameDay != null)
                entrySumOfSameDay.Duration += entry.Duration;
            else
                _togglEntrySums.Add(entry);
        }
    }
}
