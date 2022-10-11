using System.Text.Json.Serialization;

namespace AcademicTimePlanner.DataMapping.Plan
{
    public class PlanEntry
    {
        /// <summary>
        /// This class implements a single plan entry. It has a start and an end date as well as a duration and a name.
        /// An example would be name = ADS Homework, startDate = [Date of first day], endDate = [a week later], duration = 1.5 h.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="duration"></param>
        public PlanEntry(string name, DateOnly startDate, DateOnly endDate, int duration)
        {
            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
        }

        [JsonPropertyName("Id")]
        public Guid Id { get; }

        /* Will be commented back in after test
        [JsonPropertyName("TimeSpan")]
        public TimeSpan TimeSpan { get; set; }
        */

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("StartDate")]
        public DateOnly StartDate { get; set; }

        [JsonPropertyName("EndDate")]
        public DateOnly EndDate { get; set; }

        [JsonPropertyName("Duration")]
        public int Duration { get; set; }

    }
}
