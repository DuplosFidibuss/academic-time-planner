using AcademicTimePlanner.DataMapping.Toggl;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProject
    {
        private const long NoTogglId = -1;

        [JsonPropertyName("_taskList")]
        [JsonInclude]
        public List<PlanTask> _taskList;

        [JsonPropertyName("Id")]
        public Guid Id { get; }

        [JsonPropertyName("TogglProjectId")]
        public long TogglProjectId { get; set; }

        [JsonPropertyName("Name")]
        public String Name { get; set; }

        /// <summary>
        /// This class implements the plan project.
        /// The project can be linked to a <see cref="TogglProject"> Toggl project</see> but it does not have to.
        /// If no Toggle project is linked, the toggleProjectId will be -1.
        /// A project has a name and can have multiple <see cref="PlanTask"> plan tasks </see>.
        /// </summary>
        /// <param name="togglProjectId"></param>
        /// <param name="name"></param>
        [JsonConstructor]
        public PlanProject(long togglProjectId, string name)
        {
            Id = Guid.NewGuid();
            TogglProjectId = togglProjectId;
            Name = name;
            _taskList = new List<PlanTask>();
        }

        public PlanProject(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            _taskList = new List<PlanTask>();
            TogglProjectId = NoTogglId;
        }

       public void AddPlanTask(PlanTask planTask)
        {
            _taskList.Add(planTask);
        }

        public void RemovePlanTask(PlanTask planTask)
        {
            _taskList.Remove(planTask);
        }

        public double GetTotalDuration()
        {
            return GetDurationInTimeRange(DateTime.MinValue, DateTime.MaxValue);
        }

        public double GetRemainingDuration()
        {
            return GetDurationInTimeRange(DateTime.Today.AddDays(1), DateTime.MaxValue);
        }

        private double GetDurationInTimeRange(DateTime startDate, DateTime endDate)
        {
            double duration = 0;
            _taskList.ForEach(planTask => duration += planTask.GetDurationInTimeRange(startDate, endDate));
            return duration;
        }

        public SortedDictionary<DateTime,double> GetDurationsPerDateInTimeRange(DateTime startDate, DateTime endDate)
        {
            var durationsPerDateInTimeRange = new SortedDictionary<DateTime, double>();

            foreach (var entry in GetDurationsPerDate())
            {
                if (entry.Key >= startDate && entry.Key <= endDate) durationsPerDateInTimeRange.Add(entry.Key, entry.Value);
            }
            if (durationsPerDateInTimeRange.First().Value != 0 && !durationsPerDateInTimeRange.ContainsKey(startDate))
				durationsPerDateInTimeRange.Add(startDate, durationsPerDateInTimeRange.First().Value);

            durationsPerDateInTimeRange.Add(endDate.AddMilliseconds(1), durationsPerDateInTimeRange.Last().Value);
            return durationsPerDateInTimeRange;
        }

		private SortedDictionary<DateTime, double> GetDurationsPerDate()
		{
			var durationsPerDate = new SortedDictionary<DateTime, double>();
			double sum = 0;

			foreach (PlanTask planTask in _taskList)
			{
				foreach (PlanEntry entry in planTask.GetAllPlanEntriesList())
				{
					double dailyDuration = entry.Duration / ((entry.EndDate - entry.StartDate).TotalDays + 1);
					for (int i = 0; entry.StartDate.AddDays(i) <= entry.EndDate; i++)
					{
						if (durationsPerDate.ContainsKey(entry.StartDate.AddDays(i)))
						{
							durationsPerDate[entry.StartDate.AddDays(i)] += dailyDuration;
						}
						else
						{
							durationsPerDate.Add(entry.StartDate.AddDays(i).AddMilliseconds(-1), 0);
							durationsPerDate.Add(entry.StartDate.AddDays(i), dailyDuration);
						}
					}
				}
			}
			foreach (DateTime entry in durationsPerDate.Keys.ToList())
			{
				sum += durationsPerDate[entry];
				durationsPerDate[entry] = sum;
			}
			return durationsPerDate;
		}
	}
}
