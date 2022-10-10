namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntryRepetition
    {
        private Guid _id;
        private string _name;
        private DateOnly _repetitionStartDate;
        private DateOnly _repetitionEndDate;
        private int _interval;
        private int _duration;
        private LinkedList<PlanEntry> _entries;

        public PlanEntryRepetition(string name,  DateOnly repetitionStartDate, DateOnly repetitionEndDate, int interval, int duration)
        {
            _id = Guid.NewGuid();
            Name = name;
            RepetitionStartDate = repetitionStartDate;
            RepetitionEndDate = repetitionEndDate;
            Interval = interval;
            Duration = duration;
            _entries = new LinkedList<PlanEntry>();
            modify();
        }

        public string Name { get; set; }

        public DateOnly RepetitionStartDate { get; set; }
        public DateOnly RepetitionEndDate { get; set; }

        public int Interval { get; set; }

        public int Duration { get; set; }

        public void modify()
        {
            DateOnly start = _repetitionStartDate;
            DateOnly end = _repetitionEndDate;
            string name = _name;
            int i = 1;

            while(start < end)
            {
                string entryName = _name + i;
                DateOnly oldStart = start;
                start = start.AddDays(_interval);
                if (start > _repetitionEndDate) { start = _repetitionEndDate; }
                PlanEntry planEntry = new PlanEntry(entryName, oldStart, start, _duration);
                _entries.AddLast(planEntry);
            }
            

            //planEntry.Modified = true;
        }
    }
}
