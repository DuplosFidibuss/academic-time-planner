namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class ToggleProject
    {
        private Guid _id;
        private int _togglId;
        private string _name;

        public Guid Id { get { return _id; } set { _id = value; } }

        public int TogglId { get { return _togglId; } set { _togglId = value; } }

        public string Name { get { return _name; } set { _name = value; } }

        public void addTogglTask(TogglTask togglTask)
        {
            togglTask.ProjectId = _id;
        }
    }
}
