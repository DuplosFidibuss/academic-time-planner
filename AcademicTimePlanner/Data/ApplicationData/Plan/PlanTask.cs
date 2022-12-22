namespace AcademicTimePlanner.Data.ApplicationData.Plan
{
    /// <summary>
    /// This class implements the optional plan task.
    /// The task can be linked to a Toggl task in a <see cref="Toggl.TogglProject"/>
    /// </summary>
    public class PlanTask
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Dictionary<long, double> TogglIds { get; set; }

        // Private parameterless constructor used by Newtonsoft.Json for conversion.
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
