namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglProject
    {
        private Guid _id;
        private int _togglId;
        private string _name;
        private LinkedList<TogglTask> _taskList;

        public TogglProject(int togglId, string name)
        {
            _id = Guid.NewGuid();
            _togglId = togglId;
            Name = name;
            _taskList = new LinkedList<TogglTask>();
        }

        public Guid Id { 
            get { return _id; }  
        }

        public int TogglId { 
            get { return _togglId; } 
        }

        public string Name { 
            get { return _name; } 
            set { _name = value; } 
        }

        public void AddTogglTask(TogglTask togglTask)
        {
            _taskList.AddLast(togglTask);
        }

        public void RemoveTogglTask(TogglTask togglTask)
        {
            _taskList.Remove(togglTask);
        }
    }
}
