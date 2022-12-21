namespace AcademicTimePlanner.ApplicationData.Plan
{
    /// <summary>
    /// This class implements a single plan entry. It has a start and an end date as well as a duration and a name.
    /// It can have a taskId to denote if it is assigned to as planTask, if not this id is <see cref="Guid.Empty"/>.
    /// An example would be name = ADS Homework, startDate = [Date of first day], endDate = [a week later], duration = 1.5 h.
    /// </summary>
    public class PlanEntry
    {
        private static readonly Guid s_NoTaskId = Guid.Empty;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Duration { get; set; }

        public Guid TaskId { get; set; }

        // Private parameterless constructor used by Newtonsoft.Json for conversion.
        private PlanEntry() { }

        public PlanEntry(Guid id)
        {
            Id = id;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

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
            TaskId = s_NoTaskId;
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
