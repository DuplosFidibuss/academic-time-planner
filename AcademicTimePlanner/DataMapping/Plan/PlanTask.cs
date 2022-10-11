using System.Text.Json.Serialization;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanTask
    {      
        private const int NoToggleId = -1;

        [JsonPropertyName("_planEntries")]
        private LinkedList<PlanEntry> _planEntries;
        private LinkedList<PlanEntryRepetition> _repetitionEntries;

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
            _planEntries = new LinkedList<PlanEntry>();
            _repetitionEntries = new LinkedList<PlanEntryRepetition>();
        }

        public PlanTask(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            _planEntries = new LinkedList<PlanEntry>();
            _repetitionEntries = new LinkedList<PlanEntryRepetition>();
            TogglTaskId = NoToggleId;
        }

        [JsonPropertyName("Id")]
        public Guid Id { get; }

        [JsonPropertyName("TogglTaskId")]
        public int TogglTaskId { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        public void AddPlanEntry(PlanEntry planEntry)
        {
            _planEntries.AddLast(planEntry);
        }

        public void RemovePlanEntry(PlanEntry planEntry)
        {
            _planEntries.Remove(planEntry);
        }

        public void AddRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            _repetitionEntries.AddLast(planEntryRepetition);
        }

        public void RemoveRepetitionEntry(PlanEntryRepetition planEntryRepetition)
        {
            _repetitionEntries.Remove(planEntryRepetition);
        }
    }
}