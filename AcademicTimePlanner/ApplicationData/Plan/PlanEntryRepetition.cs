namespace AcademicTimePlanner.ApplicationData.Plan
{
    /// <summary>
    /// This class implements the plan entry repetition. 
    /// This is a list of <see cref="PlanEntry">plan entries</see> that repeat every interval.
    /// The repetition has a start date which corresponds with the first date of the plan entries and an end date.
    /// Such a repetition could be a semester where an entry is repeated every week. 
    /// In this case the startDate would be the first day of the semeseter and the end date would be the last one.
    /// Interval 1 week and the duration could be 2h.
    /// </summary>
    public class PlanEntryRepetition
    {
        private static readonly Guid NoTaskId = Guid.Empty;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime RepetitionStartDate { get; set; }

        public DateTime RepetitionEndDate { get; set; }

        public int Interval { get; set; }

        public double Duration { get; set; }

        public int TimeSpan { get; set; }

        public Guid TaskId { get; set; }

        public List<PlanEntry> Entries { get; set; }

        // Private parameterless constructor used by Newtonsoft.Json for conversion.
        private PlanEntryRepetition() { }

        public PlanEntryRepetition(Guid id)
        {
            Id = id;
            RepetitionStartDate = DateTime.Today;
            RepetitionEndDate = DateTime.Today.AddDays(1);
            Entries = new List<PlanEntry>();
        }

        public PlanEntryRepetition(string name, Guid taskId, DateTime repetitionStartDate, DateTime repetitionEndDate, int interval, double duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            TaskId = taskId;
            RepetitionStartDate = repetitionStartDate;
            RepetitionEndDate = repetitionEndDate;
            Interval = interval;
            Duration = duration;
            Entries = new List<PlanEntry>();
            Modify();
        }

        public PlanEntryRepetition(string name, DateTime repetitionStartDate, DateTime repetitionEndDate, int interval, double duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            TaskId = NoTaskId;
            RepetitionStartDate = repetitionStartDate;
            RepetitionEndDate = repetitionEndDate;
            Interval = interval;
            Duration = duration;
            Entries = new List<PlanEntry>();
            Modify();
        }

        public void Modify()
        {
            Entries.Clear();
            DateTime start = RepetitionStartDate;
            DateTime end = RepetitionEndDate;
            int i = 1;

            while (start < end)
            {
                string entryName = Name + i;
                i += 1;
                DateTime timeSpanEnd = start.AddDays(TimeSpan - 1);
                if (end > RepetitionEndDate)
                    end = RepetitionEndDate;
                PlanEntry planEntry = new PlanEntry(entryName, TaskId, start, timeSpanEnd, Duration);
                Entries.Add(planEntry);
                start = start.AddDays(Interval);
            }
        }

        public bool IsValidPlanEntryRepetition()
        {
            if (TimeSpan == 0)
                TimeSpan = Interval;
            return RepetitionStartDate < RepetitionEndDate
                && !string.IsNullOrWhiteSpace(Name)
                && Interval > 0
                && Interval <= (RepetitionEndDate - RepetitionStartDate).TotalDays
                && TimeSpan > 0
                && TimeSpan <= Interval
                && Duration > 0
                && Duration <= Interval * 24;
        }
    }
}
