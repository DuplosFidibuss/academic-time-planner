namespace AcademicTimePlanner.UIModels
{
    public class ProjectSelector
    {
        private const long NoTogglProjectSelected = -1;

        public Guid PlanProjectId { get; set; }

        public long TogglProjectTogglId { get; set; } = NoTogglProjectSelected;

        public bool HasValidSelection()
        {
            return !PlanProjectId.Equals(Guid.Empty) && TogglProjectTogglId != NoTogglProjectSelected;
        }
    }
}
