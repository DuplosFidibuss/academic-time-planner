using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;
using System.Text;

namespace AcademicTimePlanner.Data
{
    public class DisplayData
    {
        public List<TogglProject> LinkedTogglProjects { get; }

        public List<TogglProject> UnlinkedTogglProjects { get; }

        public List<PlanProject> PlanProjects { get; }

        public double TotalTrackedTime { get; }

        public double TotalPlannedTime { get; }

        public double RemainingDuration { get; }

        /// <summary>
        /// This class holds all data needed for the graphical overview of planning and tracked time data.
        /// </summary>
        /// <param name="allTogglProjects"></param>
        /// <param name="planProjects"></param>
        public DisplayData(List<TogglProject> allTogglProjects, List<PlanProject> planProjects)
        {
            LinkedTogglProjects = new List<TogglProject>();
            UnlinkedTogglProjects = new List<TogglProject>();
            PlanProjects = new List<PlanProject>(planProjects);

            foreach (TogglProject togglProject in allTogglProjects)
            {
                if (planProjects.Exists(planProject => planProject.TogglProjectIds.ContainsKey(togglProject.TogglId)))
                {
                    if (!LinkedTogglProjects.Contains(togglProject))
                        LinkedTogglProjects.Add(togglProject);
                }
                else
                {
                    UnlinkedTogglProjects.Add(togglProject);
                }
            }

            TotalTrackedTime = (from togglProject in LinkedTogglProjects select togglProject.GetTotalDuration()).Sum();
            TotalPlannedTime = (from planProject in planProjects select planProject.GetTotalDuration()).Sum();
            RemainingDuration = (from planProject in planProjects select planProject.GetRemainingDuration()).Sum();
        }

        public TogglProject? GetTogglProjectWithTogglId(long id)
        {
            return LinkedTogglProjects.FindLast(togglProject => togglProject.TogglId == id);
        }

        public List<TogglProject> GetAllTogglProjects()
        {
            var togglProjects = new List<TogglProject>();
            togglProjects.AddRange(LinkedTogglProjects);
            togglProjects.AddRange(UnlinkedTogglProjects);
            return togglProjects;
        }

        public string GetLinkedTogglProjectsAsString(PlanProject planProject)
        {
            var sb = new StringBuilder();
            foreach (var togglProjectId in planProject.TogglProjectIds.Keys)
            {
                var togglProject = LinkedTogglProjects.Find(project => project.TogglId == togglProjectId);
                if (togglProject != null)
                    sb.Append(togglProject.Name);
                else
                    sb.Append("Toggl project with Toggl ID " + togglProjectId + " not found");
                sb.Append(", ");
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        public string GetLinkedTogglTasksAsString(PlanProject planProject, PlanTask planTask)
        {
            var sb = new StringBuilder();
            foreach (var togglProjectId in planProject.TogglProjectIds.Keys)
            {
                var togglProject = LinkedTogglProjects.Find(project => project.TogglId == togglProjectId)!;
                foreach (var togglTask in togglProject.Tasks)
                {
                    if (planTask.TogglIds.ContainsKey(togglTask.Key))
                    {
                        sb.Append(togglTask.Value);
                        sb.Append("(" + togglProject.Name + ")");
                        sb.Append(", ");
                    }
                }
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        public string GetUnlinkedTogglProjectsAsString()
        {
            var sb = new StringBuilder();
            foreach (var togglProject in UnlinkedTogglProjects)
            {
                sb.Append(togglProject.Name);
                sb.Append(", ");
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }
    }
}
