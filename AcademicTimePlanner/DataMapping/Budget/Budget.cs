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

        public Guid Id
        {
            get { return _id; }
        }

        public string Name { 
            get { return _name; } 
            set { _name = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

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
