namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglEntrySum
    {
        private DateOnly _date;
        private int _duration;

        public DateOnly Date { get { return _date; } set { _date = value; } }
        public int Duration { get { return _duration; } set { _duration = value; } }
    }
}
