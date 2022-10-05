using AcademicTimePlanner.DataMapping.Plan;

namespace AcademicTimePlanner.DataMapping.Budget
{
    public class Budget
    {
        private Guid _id;
        private string _name;
        private int _duration;
        private bool _noBudget; //why does this exist? would this not be the name???
        private LinkedList<PlanProjectBudget> _planProjectBudgets;

        public Budget(string name, int duration) 
        { 
            _id = Guid.NewGuid();
            _name = name;
            _duration = duration;
            _noBudget = false;
            _planProjectBudgets = new LinkedList<PlanProjectBudget>();
        }

        public Guid Id
        {
            get { return _id; }
        }

        public string Name { 
            get { return _name; } 
        }

        public int Duration
        {
            get { return _duration; }
        }

        public bool NoBudget
        {
            get { return _noBudget; }   
        }

        public void AddBudget(PlanProjectBudget planProjectBudget)
        {
            _planProjectBudgets.AddLast(planProjectBudget);
        }

        public void RemoveBudget(PlanProjectBudget planProjectBudget)
        {
            _planProjectBudgets.Remove(planProjectBudget);
        }

    }
}
