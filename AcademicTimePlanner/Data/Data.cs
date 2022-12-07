using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class Data
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
        public Data(List<TogglProject> allTogglProjects, List<PlanProject> planProjects)
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
    }
}
