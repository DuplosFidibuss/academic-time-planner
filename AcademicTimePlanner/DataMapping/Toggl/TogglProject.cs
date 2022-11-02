using AcademicTimePlanner.DataMapping.Plan;

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

        public double GetTotalDuration()
        {
            return GetDurationInTimeRange(DateTime.MinValue, DateTime.MaxValue);
        }

        private double GetDurationInTimeRange(DateTime startDate, DateTime endDate)
        {
            double duration = 0;
            _taskList.ForEach(togglTask => duration += togglTask.GetDurationInTimeRange(startDate, endDate));
            return duration;
        }

        public List<double> GetDurationTime()
        {
            List<double> duration = new List<double>();
            return duration;
        }
        
        public List<DateTime> GetDurationDate()
        {
            List<DateTime> duration = new List<DateTime>();
            return duration;
        }

        private SortedDictionary<DateTime, double> GetDuration()
        {
            SortedDictionary<DateTime, double> duration = new SortedDictionary<DateTime, double>();
            double sum = 0;

            foreach (TogglTask task in _taskList)
            {
                foreach (TogglEntrySum entry in task._togglEntrySums)
                {
                    duration.Add(entry.Date, entry.Duration);
                }
                
            }
            
            foreach (DateTime entry in duration.Keys.ToList())
            {
                sum += duration[entry];
                duration[entry] = sum;
            }

            return duration;
        }

        public SortedDictionary<DateTime, double> GetDurationDictionaryInTimeRange(DateTime startDate, DateTime endDate)
        {
            SortedDictionary<DateTime, double> result = new SortedDictionary<DateTime, double>();

            foreach (var entry in GetDuration())
            {
                if (entry.Key >= startDate && entry.Key <= endDate) result.Add(entry.Key, entry.Value);
            }
            return result;
        }
    }
}
