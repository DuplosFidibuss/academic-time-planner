namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntryRepetition
    {
        private Guid _id;
        private string _name;
        private DateOnly _firstEntryStartDate;
        private DateOnly _firstEntryEndDate;
        private DateOnly _repetitionEndDate;
        private int _interval;
        private int _duration;
        private LinkedList<PlanEntry> _entries;

        public PlanEntryRepetition(string name, DateOnly firstEntryStartDate, DateOnly firtsEntryEndDate, DateOnly repetitionEndDate, int interval, int duration)
        {
            _id = Guid.NewGuid();
            Name = name;
            FirstEntryStartDate = firstEntryStartDate;
            FirstEntryEndDate = firtsEntryEndDate;
            RepetitionEndDate = repetitionEndDate;
            Interval = interval;
            Duration = duration;
            _entries = new LinkedList<PlanEntry>();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateOnly FirstEntryStartDate
        {
            get { return _firstEntryStartDate; }  
            set { _firstEntryStartDate = value; }
        }

        public DateOnly FirstEntryEndDate
        {
            get { return _firstEntryEndDate; }
            set { _firstEntryEndDate = value; }
        }

        public DateOnly RepetitionEndDate
        {
            get { return _repetitionEndDate; }
            set { _repetitionEndDate = value; }
        }

        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public void AddPlanEntry(PlanEntry planEntry)
        {
            _entries.AddLast(planEntry);
        }

        public void RemovePlanEntry(PlanEntry planEntry)
        {
            _entries.Remove(planEntry);
        }

        private void modifyPlanEntry(PlanEntry planEntry)
        {
            //TODO modify the plan entry
            planEntry.Modified = true;
        }

        public void RepeatPlanEntriy(PlanEntry planEntry)
        {
            //TODO work out an algo to do this. ( can this method and modifyPlanEntry be conbined?
            modifyPlanEntry(planEntry);
        }
    }
}
