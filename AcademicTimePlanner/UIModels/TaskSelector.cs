namespace AcademicTimePlanner.UIModels
{
    public class TaskSelector
    {
        private const long NoTogglTaskSelected = -1;

        public Guid PlanTaskId { get; set; }

        public long TogglTaskTogglId { get; set; } = NoTogglTaskSelected;

        public bool HasValidSelection()
        {
            return !PlanTaskId.Equals(Guid.Empty) && TogglTaskTogglId != NoTogglTaskSelected;
        }
    }
}
