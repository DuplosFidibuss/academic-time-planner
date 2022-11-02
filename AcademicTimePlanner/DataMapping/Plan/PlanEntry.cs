using System.Text.Json.Serialization;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntry
    {

    [JsonPropertyName("Id")]
    public Guid Id { get; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("StartDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("EndDate")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("Duration")]
    public double Duration { get; set; }
   
        /// <summary>
        /// This class implements a single plan entry. It has a start and an end date as well as a duration and a name.
        /// An example would be name = ADS Homework, startDate = [Date of first day], endDate = [a week later], duration = 1.5 h.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="duration"></param>
        [JsonConstructor]
        public PlanEntry(string name, DateTime startDate, DateTime endDate, double duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
        }
    }
}
