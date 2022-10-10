namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntry
    {
        private Guid  _id;
        private string _name;
        private DateOnly _startDate;
        private DateOnly _endDate;
        private int _duration;
        private bool _modified;
        private TimeSpan _timeSpan;

        public PlanEntry(string name, DateOnly startDate, DateOnly endDate, int duration)
        {
            _id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            Modified = false;
        }

        public Guid Id { get; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int Duration { get; set; }

        public bool Modified
        {
            get { return _modified; }
            set { _modified = true; }
        }

        public TimeSpan TimeSpan { get; set; }
    }
}
