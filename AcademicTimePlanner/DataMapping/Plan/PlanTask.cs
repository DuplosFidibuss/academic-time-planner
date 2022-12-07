namespace AcademicTimePlanner.DataMapping.Plan
{
    /// <summary>
    /// This class implements the optional plan task.
    /// The task can be linked to a Toggl task in <see cref="TogglProject"/>
    /// </summary>
    public class PlanTask
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Dictionary<long, double> TogglIds { get; set; }

        private PlanTask() { }

        public PlanTask(Guid id)
        {
            Id = id;
            TogglIds = new Dictionary<long, double>();
        }

        public bool IsValidPlanTask()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}
