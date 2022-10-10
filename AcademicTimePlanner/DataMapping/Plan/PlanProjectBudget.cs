namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProjectBudget
    {
        private Guid _id;
        private string _name;
        private int _percentage;
        private LinkedList<PlanProject> _projects;

        public PlanProjectBudget(string name)
        {
            Name = name;
            _projects = new LinkedList<PlanProject>();
            _id = Guid.NewGuid();
            _percentage = 0;
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
