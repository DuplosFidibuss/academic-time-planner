namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglEntrySum
    {
        private DateOnly _date;
        private int _duration;

        public TogglEntrySum(DateOnly date, int duration)
        {
            _date = date;
            _duration = duration;
        }

        public DateOnly Date { 
            get { return _date; } 
        }
        public int Duration { 
            get { return _duration; } 
        }
    }
}
