namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntry
    {
        private static readonly Guid NoTaskId = Guid.Empty;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Duration { get; set; }

        public Guid TaskId { get; set; }

        private PlanEntry() { }

        public PlanEntry(Guid id)
        {
            Id = id;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        /// <summary>
        /// This class implements a single plan entry. It has a start and an end date as well as a duration and a name.
        /// It can have a taskId to denote if it is assigned to as planTask, if not this id is -1.
        /// An example would be name = ADS Homework, startDate = [Date of first day], endDate = [a week later], duration = 1.5 h.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="duration"></param>
        public PlanEntry(string name, Guid taskId, DateTime startDate, DateTime endDate, double duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            TaskId = taskId;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
        }

        public PlanEntry(string name, DateTime startDate, DateTime endDate, double duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            TaskId = NoTaskId;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
        }

        public bool IsValidPlanEntry()
        {
            return StartDate <= EndDate
                && !string.IsNullOrWhiteSpace(Name)
                && Duration > 0
                && Duration <= (EndDate - StartDate).TotalHours + 24;
        }
    }
}
