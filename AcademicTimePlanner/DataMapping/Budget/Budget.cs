using AcademicTimePlanner.DataMapping.Plan;

namespace AcademicTimePlanner.DataMapping.Budget
{
    public class Budget
    {
        private LinkedList<PlanProjectBudget> _planProjectBudgets;

        public Budget(string name, int duration) 
        { 
            Id = Guid.NewGuid();
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
