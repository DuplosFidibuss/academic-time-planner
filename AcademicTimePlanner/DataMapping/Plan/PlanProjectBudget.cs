namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProjectBudget
    {
        private LinkedList<PlanProject> _projects;

        /// <summary>
        /// This class implements the conection between the <see cref="PlanProject"> plan project </see> and the <see cref="Budget.Budget"> budget </see>.
        /// 
        /// </summary>
        /// <param name="name"></param>
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
