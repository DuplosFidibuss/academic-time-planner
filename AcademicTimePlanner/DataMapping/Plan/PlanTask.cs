using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanTask
    {      
        private const int NoToggleId = -1;

        [JsonPropertyName("_planEntries")]
        [JsonInclude]
        public List<PlanEntry> _planEntries;

        [JsonPropertyName("_repetitionEntries")]
        [JsonInclude]
        public List<PlanEntryRepetition> _repetitionEntries;

        /// <summary>
        /// This class implements a plan task. 
        /// A task can be liked to a <see cref="Toggl.TogglTask"> Toggl task</see> but does not have to.
        /// If it is not linked the togglId will be set to -1.
        /// A task can have multiple <see cref="PlanEntryRepetition"> plan entry repetitions</see> and also 
        /// <see cref="PlanEntry"> plan entries</see>.
        /// Those can be added and removed.
        /// </summary>
        /// <param name="togglId"></param>
        /// <param name="name"></param>
        
        public PlanTask(int togglId, string name)
        {
            Id = Guid.NewGuid();
            TogglTaskId = togglId;
            Name = name;
            _planEntries = new List<PlanEntry>();
            _repetitionEntries = new List<PlanEntryRepetition>();
        }

        public PlanTask(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            _planEntries = new List<PlanEntry>();
            _repetitionEntries = new List<PlanEntryRepetition>();
            TogglTaskId = NoToggleId;
        }

        [JsonConstructor]
        public PlanTask() { }

        [JsonPropertyName("Id")]
        public Guid Id { get; }

        [JsonPropertyName("TogglTaskId")]
        public int TogglTaskId { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        public void AddPlanEntry(PlanEntry planEntry)
        {
            _planEntries.Add(planEntry);
        }

        public void RemovePlanEntry(PlanEntry planEntry)
        {
            _planEntries.Remove(planEntry);
        }

        public void AddRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            _repetitionEntries.Add(planEntryRepetition);
        }

        public void RemoveRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            _repetitionEntries.Remove(planEntryRepetition);
        }
    }
}