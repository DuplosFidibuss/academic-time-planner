namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntryRepetition
    {
        private const long NoTaskId = -1;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime RepetitionStartDate { get; set; }

        public DateTime RepetitionEndDate { get; set; }

        public int Interval { get; set; }

        public double Duration { get; set; }

        public long TaskId { get; set; }

        public List<PlanEntry> Entries { get; set; }

        private PlanEntryRepetition() { }

        /// <summary>
        /// This class implements the plan entry repetition. 
        /// This is a list of <see cref="PlanEntry">plan entries</see> that repeat every interval.
        /// The repetition has a start date which corresponds with the first date of the plan entries and an end date.
        /// Such a repetition could be a semester where an entrie is repeated every week. 
        /// In this case the startDate would be the first day of the semeseter and the end date would be the last one.
        /// Interval 1 week and the duration could be 2h.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="repetitionStartDate"></param>
        /// <param name="repetitionEndDate"></param>
        /// <param name="interval"></param>
        /// <param name="duration"></param>
        public PlanEntryRepetition(string name, long taskId, DateTime repetitionStartDate, DateTime repetitionEndDate, int interval, double duration)
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
                DateTime oldStart = start;
                start = start.AddDays(Interval - 1);
                if (start > RepetitionEndDate)
                    start = RepetitionEndDate;
                PlanEntry planEntry = new PlanEntry(entryName, oldStart, start, Duration);
                Entries.Add(planEntry);
                start = start.AddDays(1);
            }
        }

        public double GetDurationInTimeRange(DateTime startDate, DateTime endDate)
        {
            return (from entry in Entries.FindAll(entry => entry.StartDate >= startDate && entry.EndDate <= endDate) select entry.Duration).Sum();
        }
    }
}
