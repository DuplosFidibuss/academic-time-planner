namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglTask
    {
        private LinkedList<TogglEntrySum> _togglEntrySums;

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
            _togglEntrySums = new LinkedList<TogglEntrySum>();
        }

        public Guid Id { get; }

        public long TogglId { get; }

        public string Name { get; set; }

        public void AddEntrySum (TogglEntrySum entrySum)
        {
            _togglEntrySums.AddLast(entrySum);
        }
    }
}
