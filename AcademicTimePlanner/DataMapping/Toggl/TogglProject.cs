using System;
using System.Linq;

namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglProject
    {
        public const long NoTogglProjectId = -1;

        private List<TogglTask> _taskList;

        public Guid Id { get; }

        public long TogglId { get; }

        public string Name { get; set; }

        /// <summary>
        /// This class implements the conection between the application and TogglTrack projects.
        /// A project can have multiple <see cref="TogglTask"> Toggl tasks</see>.
        /// </summary>
        /// <param name="togglId"></param>
        /// <param name="name"></param>
        public TogglProject(long togglId, string name)
        {
            Id = Guid.NewGuid();
            TogglId = togglId;
            Name = name;
            _taskList = new List<TogglTask>();
        }

        public void AddTogglTask(TogglTask togglTask)
        {
            _taskList.Add(togglTask);
        }

        public void RemoveTogglTask(TogglTask togglTask)
        {
            _taskList.Remove(togglTask);
        }

        public TogglTask GetOrCreateTogglTask(long taskId, string taskName)
        {
            var togglTask = _taskList.FindLast(togglTask => togglTask.TogglId == taskId);
            if (togglTask == null)
            {
                togglTask = new TogglTask(taskId, taskName);
                AddTogglTask(togglTask);
            }
            return togglTask;
        }

        public int GetTotalTime()
        {
            return (from task in _taskList select task.GetTotalTime()).Sum();
        }
    }
}
