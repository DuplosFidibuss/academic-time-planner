namespace AcademicTimePlanner.Data
{
    public class DateFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateFilter(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
