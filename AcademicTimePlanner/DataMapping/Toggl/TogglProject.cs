﻿namespace AcademicTimePlanner.DataMapping.Toggl
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

        public SortedDictionary<DateTime, double> GetDurationsPerDateInTimeRange(DateTime startDate, DateTime endDate)
        {
            var durationsPerDateInTimeRange = new SortedDictionary<DateTime, double>();
            var durationsPerDate = GetDurationsPerDate();
            var dates = durationsPerDate.Keys.ToList();

            for(int i = 0; i < durationsPerDate.Count; i++)
            {
				var date = dates[i];
                if (date >= startDate && date <= endDate)
                {
                    if (durationsPerDateInTimeRange.Count == 0 && i > 0)
                    {
                        durationsPerDateInTimeRange.Add(startDate.AddMilliseconds(-1), durationsPerDate[dates[i - 1]]);
                    }
                    durationsPerDateInTimeRange.Add(date, durationsPerDate[date]);
                }
            }

            return durationsPerDateInTimeRange;
        }

		private SortedDictionary<DateTime, double> GetDurationsPerDate()
		{
			SortedDictionary<DateTime, double> durationsPerDate = new SortedDictionary<DateTime, double>();
			double sum = 0;

			foreach (var task in _taskList)
			{
				foreach (var entry in task.GetTogglEntrySums())
				{
					durationsPerDate.Add(entry.Date, 0);                                //Start of the Day
					durationsPerDate.Add(entry.Date.AddHours(23.9), entry.Duration);    //End of the Day
				}
			}

			foreach (var entry in durationsPerDate.Keys.ToList())
			{
				sum += durationsPerDate[entry];
				durationsPerDate[entry] = sum;
			}

			return durationsPerDate;
		}
	}
}
