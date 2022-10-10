using AcademicTimePlanner.DataMapping.Plan;

namespace AcademicTimePlanner.DataMapping.Budget
{
    public class Budget
    {
        private Guid _id;
        private string _name;
        private int _duration;
        private LinkedList<PlanProjectBudget> _planProjectBudgets;

        public Budget(string name, int duration) 
        { 
            _id = Guid.NewGuid();
            Name = name;
            Duration = duration;
            _planProjectBudgets = new LinkedList<PlanProjectBudget>();
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public int Duration { get; set; }

        public void AddPlanProjectBudget(PlanProjectBudget planProjectBudget)
        {
            _planProjectBudgets.AddLast(planProjectBudget);
        }

        public void RemovePlanProjectBudget(PlanProjectBudget planProjectBudget)
        {
            _planProjectBudgets.Remove(planProjectBudget);
        }

    }
}
