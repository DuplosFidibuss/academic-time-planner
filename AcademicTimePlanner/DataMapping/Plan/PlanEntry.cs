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

        public Guid Id
        {
            get { return _id; }
        }

        public string Name { 
            get { return _name; } 
            set { _name = value; }
        }

        public DateOnly StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateOnly EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public bool Modified
        {
            get { return _modified; }
            set { _modified = true; }
        }

        public TimeSpan TimeSpan
        {
            get { return _timeSpan; }
            set { _timeSpan = value; }
        }
    }
}
