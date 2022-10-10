namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanTask
    {
        /// <summary>
        /// This class implements a plan task. 
        /// A task can be liked to a <see cref="Toggl.TogglTask"> Toggl task</see> but does not have to.
        /// If it is not linked the togglId will be set to -1.
        /// A task can have multiple <see cref="PlanEntryRepetition"> plan entry repetiotions</see> and also 
        /// <see cref="PlanEntry"> plan entries</see>.
        /// Those can be added and removed.
        /// </summary>
        /// <param name="togglId"></param>
        /// <param name="name"></param>
      
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