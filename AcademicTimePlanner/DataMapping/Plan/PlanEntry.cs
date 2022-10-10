namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntry
    {
        /// <summary>
        /// This class implements a single plan entry. It has a start and an end date as well as a duration and a name.
        /// An example would be name = ADS Homework, startDate = [Date of first day], endDate = [a week later], duration = 1.5 h.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="duration"></param>
        
        public PlanEntry(string name, DateOnly startDate, DateOnly endDate, int duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
        }

        public Guid Id { get; }

        public TimeSpan TimeSpan { get; set; }

        public string Name { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int Duration { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }
}
