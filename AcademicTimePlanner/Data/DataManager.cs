using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;
using System.Text;

namespace AcademicTimePlanner.Data
{
    public class DataManager
    {
        public const string NoAssociatedPlanProjectName = "No plan project associated";

        public List<PlanProject> PlanProjects { get; set; }

        public List<TogglProject> TogglProjects { get; set; }

        public List<long> DeletedTogglProjectIds { get; set; }

        /// <summary>
        /// This class holds all data used and created by the application.
        /// </summary>
        public DataManager()
        {
            PlanProjects = new List<PlanProject>();
            TogglProjects = new List<TogglProject>();
            DeletedTogglProjectIds = new List<long>();
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
            Console.WriteLine(DeletedTogglProjectIds.Count);
        }

        public DisplayData GetDisplayData()
        {
            // This is for chart display test purposes.
            //TogglProjects.Clear();
            //TestTogglProject.GetTestTogglProject().ForEach(project => TogglProjects.Add(project));
            UpdateTogglDictionaryInPlanProjects();
            return new DisplayData(TogglProjects, PlanProjects);
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

        public void UpdatePlanningData(List<PlanProject> planProjects)
        {
            foreach (var planProject in planProjects)
            {
                var existingProject = PlanProjects.Find(project => project.Id.Equals(planProject.Id));
                if (existingProject != null)
                    PlanProjects.Remove(existingProject);
                PlanProjects.Add(planProject);
            }
        }

        public void UpdateTogglDictionaryInPlanProjects()
        {
            SortedList<int, PlanProject> sortedPlanProjects = new SortedList<int, PlanProject>();
            SortedList<int, TogglProject> sortedTogglProjects = new SortedList<int, TogglProject>();

            //fill both lists with the applicable data and give them an index to find them later.
            for (int i = 0; i < TogglProjects.Count; i++)
            {
                sortedTogglProjects.Add(i, TogglProjects[i]);
            }
            for (int i = 0; i < PlanProjects.Count; i++)
            {
                sortedPlanProjects.Add(i, PlanProjects[i]);
            }

            //fill the 2D array with the total duration of planprojects.
            //for every togglProject the total duration of the linked planProject is entered.
            double[,] mapping = fillMapping(sortedPlanProjects, sortedTogglProjects);

            //sum up all the durations per togglProject.
            double[] totalDurationSums = sumUpDurations(mapping, sortedPlanProjects, sortedTogglProjects);

            //Divide the value in the map with the summed up duration.
            divideMapEntriesBySum(mapping, totalDurationSums, sortedPlanProjects, sortedTogglProjects);
        }

        private double[,] fillMapping(SortedList<int, PlanProject> sortedPlanProjects, SortedList<int, TogglProject> sortedTogglProjects)
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

        private double[] sumUpDurations(double[,] mapping, SortedList<int, PlanProject> sortedPlanProjects, SortedList<int, TogglProject> sortedTogglProjects)
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

        private void divideMapEntriesBySum(double[,] mapping, double[] totalDurationSums, SortedList<int, PlanProject> sortedPlanProjects, SortedList<int, TogglProject> sortedTogglProjects)
        {
            foreach (int p in sortedPlanProjects.Keys)
            {
                for (int s = 0; s < totalDurationSums.Length; s++)
                {
                    if (mapping[p, s] != 0)
                    {
                        sortedPlanProjects[p].TogglProjectIds[sortedTogglProjects[s].TogglId] = mapping[p, s] / totalDurationSums[s];
                    }
                }
            }
        }

        public void DeletePlanProject(Guid planProjectId)
        {
            var planProject = PlanProjects.Find(project => project.Id == planProjectId);
            PlanProjects.Remove(planProject!);
        }
    }
}
