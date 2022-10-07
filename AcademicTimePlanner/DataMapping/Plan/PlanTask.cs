namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanTask
    {
        private Guid _id;
        private int _togglTaskId;
        private string _name;
        private LinkedList<PlanEntry> _planEntries;
        private LinkedList<PlanEntryRepetition> _repetitionEntries;
        private const int NoToggleId = -1;

        public PlanTask(int togglId, string name)
        {
            _id = Guid.NewGuid();
            _togglTaskId = togglId;
            Name = name;
            _planEntries = new LinkedList<PlanEntry>();
            _repetitionEntries = new LinkedList<PlanEntryRepetition>();
        }

        public PlanTask(string name)
        {
            _id = Guid.NewGuid();
            Name = name;
            _planEntries = new LinkedList<PlanEntry>();
            _repetitionEntries = new LinkedList<PlanEntryRepetition>();
            _togglTaskId = NoToggleId;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public int TogglId
        {
            get { return _togglTaskId; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

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