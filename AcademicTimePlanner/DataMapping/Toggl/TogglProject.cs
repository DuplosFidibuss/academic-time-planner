namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglProject
    {
        private LinkedList<TogglTask> _taskList;

        /// <summary>
        /// This class implements the conection between the application and TogglTrack projects.
        /// A project can have multiple <see cref="TogglTask"> Toggl tasks</see>.
        /// </summary>
        /// <param name="togglId"></param>
        /// <param name="name"></param>
        public TogglProject(int togglId, string name)
        {
            Id = Guid.NewGuid();
            TogglId = togglId;
            Name = name;
            _taskList = new LinkedList<TogglTask>();
        }

        public Guid Id { get; }

        public int TogglId { get; }

        public string Name { get; set; }

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
