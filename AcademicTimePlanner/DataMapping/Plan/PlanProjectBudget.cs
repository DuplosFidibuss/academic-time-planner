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

        public Guid Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }

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
