namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglTask
    {
        private Guid _id;
        private int _togglId;
        private string _name;
        private Guid _projectId;

        public Guid Id { get { return _id; } set { _id = value; } }

        public int TogglId { get { return _togglId; } set { _togglId = value; } }

        public string Name { get { return _name; } set { _name = value; } }

        public Guid ProjectId { get { return _projectId; } set { _projectId = value; } }
    }
}
