namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglProject
    {
        public const long NoTogglProjectId = -1;

        public Dictionary<long, string> Tasks { get; }

        public List<TogglEntrySum> TogglEntrySums { get; }

        public Guid Id { get; }

        public long TogglId { get; }

        public string Name { get; set; }

        /// <summary>
        /// This class implements the conection between the application and TogglTrack projects.
        /// A project can have multiple Toggl tasks</see>.
        /// </summary>
        /// <param name="togglId"></param>
        /// <param name="name"></param>
        public TogglProject(long togglId, string name)
        {
            Id = Guid.NewGuid();
            TogglId = togglId;
            Name = name;
            Tasks = new Dictionary<long, string>();
            TogglEntrySums = new List<TogglEntrySum>();
        }

        public void AddTogglTask(long togglTaskId, string name)
        {
            if (!Tasks.ContainsKey(togglTaskId))
             Tasks.Add(togglTaskId, name);
        }

        public void RemoveTogglTask(long togglTaskId)
        {
            Tasks.Remove(togglTaskId);
        }


        public void AddEntry(TogglEntrySum entry)
        {
            var entrySumOfSameDay = TogglEntrySums.FindLast(entrySum => entrySum.Date.Equals(entry.Date));
            if (entrySumOfSameDay != null)
                entrySumOfSameDay.Duration += entry.Duration;
            else
                TogglEntrySums.Add(entry);
        }

        public double GetTotalDuration()
        {
            return (from entrySum in TogglEntrySums.FindAll(entrySum => entrySum.Date >= DateTime.MinValue && entrySum.Date <= DateTime.MaxValue) select entrySum.Duration).Sum();

        }

        public SortedDictionary<DateTime, double> GetDurationsPerDateInTimeRange(DateTime startDate, DateTime endDate)
        {
            var durationsPerDateInTimeRange = new SortedDictionary<DateTime, double>();
            var durationsPerDate = GetDurationsPerDate();
            var dates = durationsPerDate.Keys.ToList();
            var startDateEntry = DateTime.MinValue;
            var endDateEntry = DateTime.MaxValue;

            for (int i = 0; i < durationsPerDate.Count; i++)
            {
                var date = dates[i];
                if (date > startDate && startDateEntry == DateTime.MinValue)
                {
                    if (i > 0)
                        startDateEntry = dates[i - 1];
                    else 
                        startDateEntry = dates[i];
                }

                if (date >= endDate && endDateEntry == DateTime.MaxValue)
                    endDateEntry = dates[i];
            }

            for (int i = 0; i < durationsPerDate.Count; i++)
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
            if (!durationsPerDateInTimeRange.ContainsKey(startDateEntry) && startDateEntry != DateTime.MinValue)
                durationsPerDateInTimeRange.Add(startDate, durationsPerDate[startDateEntry]);

            if (!durationsPerDateInTimeRange.ContainsKey(endDateEntry) && endDateEntry != DateTime.MaxValue)
                durationsPerDateInTimeRange.Add(endDate, durationsPerDate[endDateEntry]);

            return durationsPerDateInTimeRange;
        }

        private SortedDictionary<DateTime, double> GetDurationsPerDate()
        {
	        SortedDictionary<DateTime, double> durationsPerDate = new SortedDictionary<DateTime, double>();
	        double sum = 0;

	        foreach (var entry in TogglEntrySums)
		    {
			    if (!durationsPerDate.ContainsKey(entry.Date))
                    durationsPerDate.Add(entry.Date, 0);                            //Start of the Day

                if (!durationsPerDate.ContainsKey(entry.Date.AddDays(1)))
			        durationsPerDate.Add(entry.Date.AddDays(1), entry.Duration);    //End of the Day
                else
                    durationsPerDate[entry.Date.AddDays(1)] += entry.Duration;
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
