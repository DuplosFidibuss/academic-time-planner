using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProject
    {
        /// <summary>
        /// This class implements the plan project.
        /// The project can be linked to a <see cref="TogglProject"> Toggl project</see> but it does not have to.
        /// If no Toggle project islinked, the toggleProjectId will be -1.
        /// A project has a name and can have multiple <see cref="PlanTask"> plan tasks </see>.
        /// </summary>
        /// <param name="togglProjectId"></param>
        /// <param name="name"></param>

        private const int NoTogglId = -1;

        private LinkedList<PlanTask> _taskList;

        public PlanProject(int togglProjectId, string name)
        {
            Id = Guid.NewGuid();
            TogglProjectId = togglProjectId;
            Name = name;
            _taskList = new LinkedList<PlanTask>();
        }

        public PlanProject(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            _taskList = new LinkedList<PlanTask>();
            TogglProjectId = NoTogglId;
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
