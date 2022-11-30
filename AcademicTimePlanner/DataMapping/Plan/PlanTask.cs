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

        public long TogglId { get; set; }

        private PlanTask() { }

        public PlanTask(Guid id, string name, long togglId)
        {
            Id = id;
            Name = name;
            TogglId = togglId;
        }

        public PlanTask(string name, long togglId)
        {
            Id = Guid.NewGuid();
            Name = name;
            TogglId = togglId;
        }

        public PlanTask(Guid id)
        {
            Id = id;
        }

        public bool IsValidPlanTask()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}
