namespace AcademicTimePlanner.ApplicationData.Plan
{
    /// <summary>
    /// This class implements the plan project.
    /// The project can be linked to multiple <see cref="Toggl.TogglProject">Toggl projects</see> but it does not have to.
    /// A project has a name and can have multiple <see cref="PlanTask">plan tasks</see>.
    /// </summary>
    public class PlanProject
    {
        public Guid Id { get; set; }

        public Dictionary<long, double> TogglProjectIds { get; set; }

        public string Name { get; set; }

        public List<PlanTask> PlanTasks { get; set; }

        public List<PlanEntry> PlanEntries { get; set; }

        public List<PlanEntryRepetition> RepetitionEntries { get; set; }

        // Private parameterless constructor used by Newtonsoft.Json for conversion.
        private PlanProject() { }

        public PlanProject(Guid id)
        {
            Id = id;
            TogglProjectIds = new Dictionary<long, double>();
            PlanTasks = new List<PlanTask>();
            PlanEntries = new List<PlanEntry>();
            RepetitionEntries = new List<PlanEntryRepetition>();
        }

        public PlanProject(Dictionary<long, double> togglProjectIds, string name)
        {
            Id = Guid.NewGuid();
            TogglProjectIds = togglProjectIds;
            Name = name;
            PlanTasks = new List<PlanTask>();
            PlanEntries = new List<PlanEntry>();
            RepetitionEntries = new List<PlanEntryRepetition>();
        }

        public PlanProject(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            PlanTasks = new List<PlanTask>();
            TogglProjectIds = new Dictionary<long, double>();
            PlanEntries = new List<PlanEntry>();
            RepetitionEntries = new List<PlanEntryRepetition>();
        }

        public void AddPlanTask(PlanTask planTask)
        {
            PlanTasks.Add(planTask);
        }

        public void RemovePlanTask(PlanTask planTask)
        {
            PlanTasks.Remove(planTask);
        }

        public void AddPlanEntry(PlanEntry planEntry)
        {
            PlanEntries.Add(planEntry);
        }

        public void RemovePlanEntry(PlanEntry planEntry)
        {
            PlanEntries.Remove(planEntry);
        }

        public void AddPlanEntryRepetition(PlanEntryRepetition planEntryRepetition)
        {
            RepetitionEntries.Add(planEntryRepetition);
        }

        public void RemovePlanEntryRepetition(PlanEntryRepetition planEntryRepetition)
        {
            RepetitionEntries.Remove(planEntryRepetition);
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
            return
            (from planEntry
             in GetAllPlanEntriesList()
             select planEntry.Duration)
             .Sum();
        }

        public double GetRemainingDuration()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            double sum = 0;
            foreach (var entry in GetAllPlanEntriesList())
            {
                if (entry.StartDate < tomorrow && entry.EndDate < tomorrow)
                    continue;

                else if (entry.StartDate < tomorrow && entry.EndDate >= tomorrow)
                    sum += entry.Duration * ((entry.EndDate - tomorrow).TotalDays + 1) / ((entry.EndDate - entry.StartDate).TotalDays + 1);

                else
                    sum += entry.Duration;
            }
            return sum;
        }

        public SortedDictionary<DateTime, double> GetDurationsPerDateInTimeRange(DateTime startDate, DateTime endDate)
        {
            var durationsPerDateInTimeRange = new SortedDictionary<DateTime, double>();

            foreach (var entry in GetDurationsPerDate())
            {
                if (entry.Key >= startDate && entry.Key <= endDate)
                    durationsPerDateInTimeRange.Add(entry.Key, entry.Value);
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
                        if (durationsPerDate.Count == 0 || !durationsPerDate.ContainsKey(entry.StartDate))
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
