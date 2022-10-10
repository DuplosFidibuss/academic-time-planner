namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProjectBudget
    {
        private LinkedList<PlanProject> _projects;

        public PlanProjectBudget(string name)
        {
            Name = name;
            _projects = new LinkedList<PlanProject>();
            Id = Guid.NewGuid();
            Percentage = 0;
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public int Percentage { get; set; }

        public void AddPlanProject(PlanProject planProject)
        {
            _projects.AddLast(planProject);
        }

        public void RemovePlanProject(PlanProject planProject)
        {
            _projects.Remove(planProject);
        }

    }
}
