﻿using AcademicTimePlanner.DataMapping.Toggl;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProject
    {
        private const long NoTogglId = -1;

        [JsonPropertyName("Id")]
        public Guid Id { get; }

        [JsonPropertyName("TogglProjectId")]
        public long TogglProjectId { get; set; }

        [JsonPropertyName("Name")]
        public String Name { get; set; }

        [JsonPropertyName("Tasks")]
        [JsonInclude]
        public Dictionary<long, string>? Tasks { get; set; }

        [JsonPropertyName("PlanEntries")]
        [JsonInclude]
        public List<PlanEntry> PlanEntries { get; set; }

        [JsonPropertyName("RepetitionEntries")]
        [JsonInclude]
        public List<PlanEntryRepetition> RepetitionEntries { get; set; }


        /// <summary>
        /// This class implements the plan project.
        /// The project can be linked to a <see cref="TogglProject"> Toggl project</see> but it does not have to.
        /// If no Toggle project is linked, the toggleProjectId will be -1.
        /// A project has a name and can have multiple plan tasks </see>.
        /// </summary>
        /// <param name="togglProjectId"></param>
        /// <param name="name"></param>
        [JsonConstructor]
        public PlanProject(long togglProjectId, string name)
        {
            Id = Guid.NewGuid();
            TogglProjectId = togglProjectId;
            Name = name;
            Tasks = new Dictionary<long, string>();
            PlanEntries = new List<PlanEntry>();
            RepetitionEntries = new List<PlanEntryRepetition>();
        }

        public PlanProject(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Tasks = new Dictionary<long, string>();
            TogglProjectId = NoTogglId;
            PlanEntries = new List<PlanEntry>();
            RepetitionEntries = new List<PlanEntryRepetition>();
        }

       public void AddPlanTask(long taskId, string name)
        {
            if (!Tasks.ContainsKey(taskId))
                Tasks.Add(taskId, name);
        }

        public void RemovePlanTask(long taskId)
        {
            Tasks.Remove(taskId);
        }

        public void AddPlanEntry(PlanEntry planEntry)
        {
            PlanEntries.Add(planEntry);
        }

        public void RemovePlanEntry(PlanEntry planEntry)
        {
            PlanEntries.Remove(planEntry);
        }

        public void AddRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            RepetitionEntries.Add(planEntryRepetition);
        }

        public void RemoveRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            RepetitionEntries.Remove(planEntryRepetition);
        }

        private double GetDurationInTimeRange(DateTime startDate, DateTime endDate)
        {
            if (PlanEntries == null && RepetitionEntries == null)
                return 0;

            if (PlanEntries == null)
                return (from repetitionEntry in RepetitionEntries select repetitionEntry.GetDurationInTimeRange(startDate, endDate)).Sum();

            if (RepetitionEntries == null)
                return (from planEntry in PlanEntries.FindAll(planEntry => planEntry.StartDate >= startDate && planEntry.EndDate <= endDate) select planEntry.Duration).Sum();

            return (from planEntry in PlanEntries.FindAll(planEntry => planEntry.StartDate >= startDate && planEntry.EndDate <= endDate) select planEntry.Duration).Sum() +
                    (from repetitionEntry in RepetitionEntries select repetitionEntry.GetDurationInTimeRange(startDate, endDate)).Sum();
        }

        private List<PlanEntry> GetAllPlanEntriesList()
        {
            var planEntries = new List<PlanEntry>();

            if (RepetitionEntries == null && PlanEntries == null)
            {
                planEntries.Add(new PlanEntry("NoEntries", DateTime.Today, DateTime.Today, 0));
                return planEntries;
            }

            if (RepetitionEntries != null)
            {
                foreach (PlanEntryRepetition planEntryRepetition in RepetitionEntries)
                {
                    planEntries.AddRange(planEntryRepetition.Entries);
                }
            }

            if (PlanEntries != null)
                planEntries.AddRange(PlanEntries);

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
