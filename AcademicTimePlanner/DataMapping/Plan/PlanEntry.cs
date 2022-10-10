namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntry
    {
        public PlanEntry(string name, DateOnly startDate, DateOnly endDate, int duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int Duration { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }
}
