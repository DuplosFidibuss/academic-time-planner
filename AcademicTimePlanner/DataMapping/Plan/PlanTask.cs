namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanTask
    {
        private const int NoToggleId = -1;

        private LinkedList<PlanEntry> _planEntries;
        private LinkedList<PlanEntryRepetition> _repetitionEntries;

        public PlanTask(int togglId, string name)
        {
            Id = Guid.NewGuid();
            TogglTaskId = togglId;
            Name = name;
            _planEntries = new LinkedList<PlanEntry>();
            _repetitionEntries = new LinkedList<PlanEntryRepetition>();
        }

        public PlanTask(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            _planEntries = new LinkedList<PlanEntry>();
            _repetitionEntries = new LinkedList<PlanEntryRepetition>();
            TogglTaskId = NoToggleId;
        }

        public Guid Id { get; }

        public int TogglTaskId { get; set; }
     
        public string Name { get; set; }

        public void AddPlanEntry(PlanEntry planEntry)
        {
            _planEntries.AddLast(planEntry);
        }

        public void RemovePlanEntry(PlanEntry planEntry)
        {
            _planEntries.Remove(planEntry);
        }

        public void AddRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            _repetitionEntries.AddLast(planEntryRepetition);
        }

        public void RemoveRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            _repetitionEntries.Remove(planEntryRepetition);
        }
    }
}