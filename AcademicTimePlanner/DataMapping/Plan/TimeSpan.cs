namespace AcademicTimePlanner.DataMapping.Plan
{
    public class TimeSpan
    {

        public TimeSpan(DateOnly startDate, DateTime endDate) {
            Id = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid Id { get; }
        
        public DateOnly StartDate { get; }

        public DateTime EndDate { get; }
    }
}
