using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProject
    {
        private const int NoTogglId = -1;

        private Guid _id;
        private int _togglProjectId;
        private string _name;
        private LinkedList<PlanTask> _taskList;

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

        public Guid Id { get; }

        public int TogglProjectId { get; set; }

        public String Name { get; set; }

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
