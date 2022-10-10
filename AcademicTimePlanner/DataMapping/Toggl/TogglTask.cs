namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglTask
    {
        private Guid _id;
        private int _togglId;
        private string _name;
        private LinkedList<TogglEntrySum> _togglEntrySums;

        public TogglTask(int togglId, string name)
        {
            _id = Guid.NewGuid();
            _togglId = togglId;
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
