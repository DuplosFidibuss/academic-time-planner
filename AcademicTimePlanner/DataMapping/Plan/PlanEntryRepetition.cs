namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntryRepetition
    {
        private Guid _id;
        private string _name;
        private DateOnly _repetitionStartDate;
        private DateOnly _repetitionEndDate;
        private int _interval;
        private int _duration;
        private LinkedList<PlanEntry> _entries;

        public PlanEntryRepetition(string name,  DateOnly repetitionStartDate, DateOnly repetitionEndDate, int interval, int duration, LinkedList<PlanEntry> planEntries)
        {
            _id = Guid.NewGuid();
            Name = name;
            RepetitionStartDate = repetitionStartDate;
            RepetitionEndDate = repetitionEndDate;
            Interval = interval;
            Duration = duration;
            _entries = planEntries;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateOnly RepetitionStartDate { get; set; }
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
