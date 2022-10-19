using AcademicTimePlanner.DataMapping.Plan;

namespace AcademicTimePlanner.DataMapping.Budget
{
    public class Budget
    {
        private List<PlanProjectBudget> _planProjectBudgets;
        public Guid Id { get; }

        public string Name { get; set; }

        public int Duration { get; set; }

        /// <summary>
        /// This is an optional class for better management of non uniform projects.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="duration"></param>
        public Budget(string name, int duration) 
        { 
            Id = Guid.NewGuid();
            Name = name;
            Duration = duration;
            _planProjectBudgets = new List<PlanProjectBudget>();
        }
        public void AddPlanProjectBudget(PlanProjectBudget planProjectBudget)
        {
            _planProjectBudgets.Add(planProjectBudget);
        }

        public void RemovePlanProjectBudget(PlanProjectBudget planProjectBudget)
        {
            _planProjectBudgets.Remove(planProjectBudget);
        }

    }
}
