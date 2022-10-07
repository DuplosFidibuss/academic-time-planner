using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProject
    {
        private Guid _id;
        private int _togglProjectId;
        private string _name;
        private LinkedList<PlanTask> _taskList;
        private const int NoTogglId = -1;

        public PlanProject(int togglProjectId, string name)
        {
            _id = Guid.NewGuid();
            _togglProjectId = togglProjectId;
            Name = name;
            _taskList = new LinkedList<PlanTask>();
        }

        public PlanProject(string name)
        {
            _id = Guid.NewGuid();
            Name = name;
            _taskList = new LinkedList<PlanTask>();
            _togglProjectId = NoTogglId;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public int TogglProjectId { 
            get { return _togglProjectId; } 
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

       public void AddPlanTask(PlanTask planTask)
        {
            _taskList.AddLast(planTask);
        }

        public void RemovePlanTask(PlanTask planTask)
        {
            _taskList.Remove(planTask);
        }

    }
}
