namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglEntrySum
    {
        private DateOnly _date;
        private int _duration;
        private int _togglId;
        private int _togglTaskId;

        public TogglEntrySum(DateOnly date, int duration, int togglId, int togglTaskId)
        {
            _date = date;
            _duration = duration;
            _togglId = togglId;
            _togglTaskId = togglTaskId;
        }

        public DateOnly Date { get; }
        public int Duration { get; } 
        public int TogglId { get; }
        public int TogglTaskId { get; }
    }
}
