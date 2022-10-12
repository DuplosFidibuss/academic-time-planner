namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntryRepetition
    {
        private List<PlanEntry> _entries;

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
        public PlanEntryRepetition(string name,  DateTime repetitionStartDate, DateTime repetitionEndDate, int interval, int duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            RepetitionStartDate = repetitionStartDate;
            RepetitionEndDate = repetitionEndDate;
            Interval = interval;
            Duration = duration;
            _entries = new List<PlanEntry>();
            modify();
        }


        public Guid Id { get; }

        public TimeSpan TimeSpan { get; set; }

        public string Name { get; set; }

        public DateTime RepetitionStartDate { get; set; }

        public DateTime RepetitionEndDate { get; set; }

        public int Interval { get; set; }

        public int Duration { get; set; }

        public void modify()
        {
            _entries.Clear();
            DateTime start = RepetitionStartDate;
            DateTime end = RepetitionEndDate;
            int i = 1;

            while(start < end)
            {
                string entryName = Name + i;
                DateTime oldStart = start;
                start = start.AddDays(Interval-1);
                if (start > RepetitionEndDate) 
                    start = RepetitionEndDate; 
                PlanEntry planEntry = new PlanEntry(entryName, oldStart, start, Duration);
                _entries.Add(planEntry);
                start = start.AddDays(1);
            }
        }
    }
}
