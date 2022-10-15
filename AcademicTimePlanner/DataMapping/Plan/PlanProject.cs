using AcademicTimePlanner.DataMapping.Toggl;
using System.Text.Json.Serialization;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanProject
    {

        private const long NoTogglId = -1;

        [JsonPropertyName("_taskList")]
        [JsonInclude]
        public List<PlanTask> _taskList;

        [JsonPropertyName("Id")]
        public Guid Id { get; }

        [JsonPropertyName("TogglProjectId")]
        public long TogglProjectId { get; set; }

        [JsonPropertyName("Name")]
        public String Name { get; set; }

        /// <summary>
        /// This class implements the plan project.
        /// The project can be linked to a <see cref="TogglProject"> Toggl project</see> but it does not have to.
        /// If no Toggle project is linked, the toggleProjectId will be -1.
        /// A project has a name and can have multiple <see cref="PlanTask"> plan tasks </see>.
        /// </summary>
        /// <param name="togglProjectId"></param>
        /// <param name="name"></param>
        [JsonConstructor]
        public PlanProject(long togglProjectId, string name)
        {
            Id = Guid.NewGuid();
            TogglProjectId = togglProjectId;
            Name = name;
            _taskList = new List<PlanTask>();
        }

        public PlanProject(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            _taskList = new List<PlanTask>();
            TogglProjectId = NoTogglId;
        }

       public void AddPlanTask(PlanTask planTask)
        {
            _taskList.Add(planTask);
        }

        public void RemovePlanTask(PlanTask planTask)
        {
            _taskList.Remove(planTask);
        }

    }
}
