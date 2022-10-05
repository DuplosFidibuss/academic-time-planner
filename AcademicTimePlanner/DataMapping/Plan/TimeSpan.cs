namespace AcademicTimePlanner.DataMapping.Plan
{
    public class TimeSpan
    {
        private Guid _id;
        private DateOnly _startDate;
        private DateTime _endDate;

        public TimeSpan(DateOnly startDate, DateTime endDate) {
            _id = Guid.NewGuid();
            _startDate = startDate;
            _endDate = endDate;
        }
        
        public DateOnly Startdate
        {
            get { return _startDate; }
        }

        public DateTime Enddate
        {
            get { return _endDate; }
        }
    }
}
