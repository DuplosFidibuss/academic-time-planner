using AcademicTimePlanner.DataMapping.Toggl;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProject
    {
        private const long NoTogglId = -1;

        [JsonPropertyName("_planEntries")]
        [JsonInclude]
        public List<PlanEntry> _planEntries;

        [JsonPropertyName("_repetitionEntries")]
        [JsonInclude]
        public List<PlanEntryRepetition> _repetitionEntries;

        [JsonPropertyName("_taskList")]
        [JsonInclude]
        public Dictionary<long, string>? _taskList;

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
            _taskList = new Dictionary<long, string>();
            _planEntries = new List<PlanEntry>();
            _repetitionEntries = new List<PlanEntryRepetition>();
        }

        public PlanProject(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            _taskList = new Dictionary<long, string>();
            TogglProjectId = NoTogglId;
            _planEntries = new List<PlanEntry>();
            _repetitionEntries = new List<PlanEntryRepetition>();
        }

       public void AddPlanTask(long taskId, string name)
        {
            if (!_taskList.ContainsKey(taskId))
                _taskList.Add(taskId, name);
        }

        public void RemovePlanTask(long taskId)
        {
            _taskList.Remove(taskId);
        }

        public void AddPlanEntry(PlanEntry planEntry)
        {
            _planEntries.Add(planEntry);
        }

        public void RemovePlanEntry(PlanEntry planEntry)
        {
            _planEntries.Remove(planEntry);
        }

        public void AddRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            _repetitionEntries.Add(planEntryRepetition);
        }

        public void RemoveRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            _repetitionEntries.Remove(planEntryRepetition);
        }

        public double GetDurationInTimeRange(DateTime startDate, DateTime endDate)
        {
            if (_planEntries == null && _repetitionEntries == null)
                return 0;

            if (_planEntries == null)
                return (from repetitionEntry in _repetitionEntries select repetitionEntry.GetDurationInTimeRange(startDate, endDate)).Sum();

            if (_repetitionEntries == null)
                return (from planEntry in _planEntries.FindAll(planEntry => planEntry.StartDate >= startDate && planEntry.EndDate <= endDate) select planEntry.Duration).Sum();

            return (from planEntry in _planEntries.FindAll(planEntry => planEntry.StartDate >= startDate && planEntry.EndDate <= endDate) select planEntry.Duration).Sum() +
                    (from repetitionEntry in _repetitionEntries select repetitionEntry.GetDurationInTimeRange(startDate, endDate)).Sum();
        }

        public List<PlanEntry> GetAllPlanEntriesList()
        {
            var planEntries = new List<PlanEntry>();

            if (_repetitionEntries == null && _planEntries == null)
            {
                planEntries.Add(new PlanEntry("NoEntries", DateTime.Today, DateTime.Today, 0));
                return planEntries;
            }

            if (_repetitionEntries != null)
            {
                foreach (PlanEntryRepetition planEntryRepetition in _repetitionEntries)
                {
                    planEntries.AddRange(planEntryRepetition.Entries);
                }
            }

            if (_planEntries != null)
                planEntries.AddRange(_planEntries);

            return planEntries;
        }

        public double GetTotalDuration()
        {
            return GetDurationInTimeRange(DateTime.MinValue, DateTime.MaxValue);
        }

        public double GetRemainingDuration()
        {
            return GetDurationInTimeRange(DateTime.Today.AddDays(1), DateTime.MaxValue);
        }

        public SortedDictionary<DateTime,double> GetDurationsPerDateInTimeRange(DateTime startDate, DateTime endDate)
        {
            var durationsPerDateInTimeRange = new SortedDictionary<DateTime, double>();

            foreach (var entry in GetDurationsPerDate())
            {
                if (entry.Key >= startDate && entry.Key <= endDate) durationsPerDateInTimeRange.Add(entry.Key, entry.Value);
            }

            return durationsPerDateInTimeRange;
        }

        private SortedDictionary<DateTime, double> GetDurationsPerDate()
        {
	        var durationsPerDate = new SortedDictionary<DateTime, double>();
	        double sum = 0;

	        foreach (PlanEntry entry in GetAllPlanEntriesList())
		    {
			    double dailyDuration = entry.Duration / ((entry.EndDate - entry.StartDate).TotalDays + 1);
			    for (int i = 1; entry.StartDate.AddDays(i) <= entry.EndDate.AddDays(1); i++)
			    {
				    if (durationsPerDate.ContainsKey(entry.StartDate.AddDays(i)))
				    {
					    durationsPerDate[entry.StartDate.AddDays(i)] += dailyDuration;
				    }
				    else
                    { 
                        if(durationsPerDate.Count == 0)
                            durationsPerDate.Add(entry.StartDate, 0);
					    durationsPerDate.Add(entry.StartDate.AddDays(i), dailyDuration);
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
