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

        public Guid Id { get; }
        
        public DateOnly Startdate { get; }

        public DateTime Enddate { get; }
    }
}
