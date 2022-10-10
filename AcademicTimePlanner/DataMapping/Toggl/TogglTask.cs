namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglTask
    {
        private LinkedList<TogglEntrySum> _togglEntrySums;

        public TogglTask(int togglId, string name)
        {
            Id = Guid.NewGuid();
            TogglId = togglId;
            Name = name;
            _togglEntrySums = new LinkedList<TogglEntrySum>();
        }

        public Guid Id { get; }

        public int TogglId { get; }

        public string Name { get; set; }

        public void AddEntrySum (TogglEntrySum entrySum)
        {
            _togglEntrySums.AddLast(entrySum);
        }
    }
}
