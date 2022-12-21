namespace AcademicTimePlanner.UIModels
{
    public class DateFilter
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateFilter()
        {
            StartDate = DateTime.Today.AddDays(-30);
            EndDate = DateTime.Today;
        }

        public bool IsValidTimeRange()
        {
            return StartDate < EndDate;
        }
    }
}
