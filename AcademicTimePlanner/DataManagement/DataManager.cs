using AcademicTimePlanner.ApplicationData.Plan;
using AcademicTimePlanner.ApplicationData.Toggl;
using AcademicTimePlanner.DisplayData;
using System.Text;

namespace AcademicTimePlanner.DataManagement
{
    /// <summary>
    /// This class holds all data used and created by the application.
    /// </summary>
    public class DataManager
    {
        public const string NoAssociatedPlanProjectName = "No plan project associated";

        public List<PlanProject> PlanProjects { get; set; }

        public List<TogglProject> TogglProjects { get; set; }

        public List<long> DeletedTogglProjectIds { get; set; }

        public DataManager()
        {
            PlanProjects = new List<PlanProject>();
            TogglProjects = new List<TogglProject>();
            DeletedTogglProjectIds = new List<long>();
        }

        public ProjectsData GetProjectsData()
        {
            return new ProjectsData(TogglProjects, PlanProjects);
        }

        public List<TogglLoadOverviewData> GetTogglLoadOverview()
        {
            var loadOverview = new List<TogglLoadOverviewData>();
            foreach (var togglProject in TogglProjects)
            {
                var planProjects = PlanProjects.FindAll(project => project.TogglProjectIds.ContainsKey(togglProject.TogglId));
                var builder = new StringBuilder();
                foreach (var project in planProjects)
                {
                    builder.Append(project.Name);
                    builder.Append(',');
                }

                if (builder.Length > 0)
                {
                    builder.Remove(builder.Length - 1, 1);
                    loadOverview.Add(new TogglLoadOverviewData(togglProject.Name, DeletedTogglProjectIds.Contains(togglProject.TogglId), builder.ToString()));
                }
                else
                {
                    loadOverview.Add(new TogglLoadOverviewData(togglProject.Name, DeletedTogglProjectIds.Contains(togglProject.TogglId), NoAssociatedPlanProjectName));
                }
            }
            return loadOverview;
        }

        public void UpdatePlanProject(PlanProject planProject)
        {
            var existingProject = PlanProjects.Find(project => project.Id == planProject.Id);

            if (existingProject != null)
                PlanProjects.Remove(existingProject);
            PlanProjects.Add(planProject);

            UpdateTogglDictionaryInPlanProjects();
        }

        public void DeletePlanProject(Guid planProjectId)
        {
            var planProject = PlanProjects.Find(project => project.Id == planProjectId);
            PlanProjects.Remove(planProject!);
            UpdateTogglDictionaryInPlanProjects();
        }

        public void UpdateTogglData(List<TogglProject> togglProjects)
        {
            var currentTogglProjects = new List<TogglProject>(TogglProjects);
            TogglProjects.Clear();
            DeletedTogglProjectIds.Clear();

            foreach (var project in togglProjects)
            {
                TogglProjects.Add(project);
            }

            foreach (var currentProject in currentTogglProjects)
            {
                if (!togglProjects.Any(project => project.TogglId == currentProject.TogglId))
                {
                    TogglProjects.Add(currentProject);
                    DeletedTogglProjectIds.Add(currentProject.TogglId);
                }
            }

            UpdateTogglDictionaryInPlanProjects();
        }

        public void UpdatePlanningData(List<PlanProject> planProjects)
        {
            foreach (var planProject in planProjects)
            {
                var existingProject = PlanProjects.Find(project => project.Id.Equals(planProject.Id));
                if (existingProject != null)
                    PlanProjects.Remove(existingProject);
                PlanProjects.Add(planProject);
            }

            UpdateTogglDictionaryInPlanProjects();
        }

        public void UpdateTogglDictionaryInPlanProjects()
        {
            var sortedPlanProjects = new SortedList<int, PlanProject>();
            var sortedTogglProjects = new SortedList<int, TogglProject>();

            // Fill both lists with the applicable data and give them an index to find them later.
            for (int i = 0; i < TogglProjects.Count; i++)
            {
                sortedTogglProjects.Add(i, TogglProjects[i]);
            }
            for (int i = 0; i < PlanProjects.Count; i++)
            {
                sortedPlanProjects.Add(i, PlanProjects[i]);
            }

            // Fill the 2D array with the total duration of PlanProjects.
            // For every TogglProject the total duration of the linked PlanProject is entered.
            var mapping = FillMapping(sortedPlanProjects, sortedTogglProjects);

            // Sum up all the durations per TogglProject.
            var totalDurationSums = SumUpDurations(mapping, sortedPlanProjects, sortedTogglProjects);

            // Divide the value in the map with the summed up duration.
            DivideMapEntriesBySum(mapping, totalDurationSums, sortedPlanProjects, sortedTogglProjects);
        }

        private double[,] FillMapping(SortedList<int, PlanProject> sortedPlanProjects, SortedList<int, TogglProject> sortedTogglProjects)
        {
            double[,] mapping = new double[sortedPlanProjects.Count, sortedTogglProjects.Count];

            foreach (int p in sortedPlanProjects.Keys)
            {
                double duration = sortedPlanProjects[p].GetTotalDuration();
                foreach (int t in sortedTogglProjects.Keys)
                {
                    if (sortedPlanProjects[p].TogglProjectIds.ContainsKey(sortedTogglProjects[t].TogglId))
                        mapping[p, t] = duration;
                }
            }
            return mapping;
        }

        private double[] SumUpDurations(double[,] mapping, SortedList<int, PlanProject> sortedPlanProjects, SortedList<int, TogglProject> sortedTogglProjects)
        {
            double[] totalDurationSums = new double[sortedTogglProjects.Count];

            foreach (int t in sortedTogglProjects.Keys)
            {
                foreach (int p in sortedPlanProjects.Keys)
                {
                    totalDurationSums[t] += mapping[p, t];
                }
            }
            return totalDurationSums;
        }

        private void DivideMapEntriesBySum(double[,] mapping, double[] totalDurationSums, SortedList<int, PlanProject> sortedPlanProjects, SortedList<int, TogglProject> sortedTogglProjects)
        {
            foreach (int p in sortedPlanProjects.Keys)
            {
                for (int s = 0; s < totalDurationSums.Length; s++)
                {
                    if (mapping[p, s] != 0)
                        sortedPlanProjects[p].TogglProjectIds[sortedTogglProjects[s].TogglId] = mapping[p, s] / totalDurationSums[s];
                }
            }
        }
    }
}
