using System.Text.Json.Serialization;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntryRepetition
    {
        [JsonPropertyName("_entries")]
        [JsonInclude]
        public List<PlanEntry> Entries { get; }

        [JsonPropertyName("Id")]
        public Guid Id { get; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("RepetitionStartDate")]
        public DateTime RepetitionStartDate { get; set; }

        [JsonPropertyName("RepetitionEndDate")]
        public DateTime RepetitionEndDate { get; set; }

        [JsonPropertyName("Interval")]
        public int Interval { get; set; }

        [JsonPropertyName("Duration")]
        public double Duration { get; set; }

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

        [JsonConstructor]
        public PlanEntryRepetition(string name,  DateTime repetitionStartDate, DateTime repetitionEndDate, int interval, double duration)
        {
            Id = Guid.NewGuid();
            Name = name;
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

            while(start < end)
            {
                string entryName = Name + i;
                i += 1;
                DateTime oldStart = start;
                start = start.AddDays(Interval-1);
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
