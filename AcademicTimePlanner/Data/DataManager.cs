using AcademicTimePlanner.DataMapping.Budget;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class DataManager
    {
        public const string NoAssociatedPlanProjectName = "No plan project associated";

        public List<Budget> Budgets { get; set; }

        public List<PlanProject> PlanProjects { get; set; }

        public List<TogglProject> TogglProjects { get; set; }

        public List<long> DeletedTogglProjectIds { get; set; }

        /// <summary>
        /// This class holds all data used and created by the application.
        /// </summary>
        public DataManager()
        {
            Budgets = new List<Budget>();
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

        public ChartData GetChartData()
        {
            // This is for chart display test purposes.
            //TogglProjects.Clear();
            //TestTogglProject.GetTestTogglProject().ForEach(project => TogglProjects.Add(project));
            UpdateTogglDictionaryInPlanProjects();
            return new ChartData(TogglProjects, PlanProjects);
        }

        public List<TogglLoadOverviewData> GetTogglLoadOverview()
        {
            var loadOverview = new List<TogglLoadOverviewData>();
            foreach (var togglProject in TogglProjects)
            {
                var planProject = PlanProjects.Find(project => project.TogglProjectIds.ContainsKey(togglProject.TogglId));
                var planProjectName = planProject != null ? planProject.Name : NoAssociatedPlanProjectName;
                var projectOverviewData = new TogglLoadOverviewData(togglProject.Name, DeletedTogglProjectIds.Contains(togglProject.TogglId), planProjectName);
                loadOverview.Add(projectOverviewData);
            }
            return loadOverview;
        }

        public void UpdatePlanningData(List<PlanProject> planProjects)
        {
            PlanProjects.Clear();
            PlanProjects.AddRange(planProjects);
        }

        public void UpdateTogglDictionaryInPlanProjects()
        {
            SortedList<int, PlanProject> sortedPlanProjects = new SortedList<int, PlanProject>();
            SortedList<int, TogglProject> sortedTogglProjects = new SortedList<int, TogglProject>();

            //fill both lists with the applicable data and give them an index to find them later.
            for(int i = 0; i < TogglProjects.Count; i++)
            {
                sortedTogglProjects.Add(i, TogglProjects[i]);
            }
            for(int i = 0; i < PlanProjects.Count; i++)
            {
                sortedPlanProjects.Add(i, PlanProjects[i]);
            }

            double[,] mapping = new double[sortedPlanProjects.Count, sortedTogglProjects.Count];

            //fill the 2D array with the total duration of planprojects.
            //for every togglProject the total duration of the linked planProject is entered.
            foreach(int p in sortedPlanProjects.Keys)
            {
                double duration = sortedPlanProjects[p].GetTotalDuration();
                foreach (int t in sortedTogglProjects.Keys)
                {
                    if (sortedPlanProjects[p].TogglProjectIds.ContainsKey(sortedTogglProjects[t].TogglId))
                        mapping[p,t] = duration;
                }
            }

            List<double> totalDurationSums = new List<double>(sortedTogglProjects.Count);

            //sum up all the durations per togglProject.
            foreach(int t in sortedTogglProjects.Keys)
            {
                foreach(int p in sortedPlanProjects.Keys)
                {
                    totalDurationSums[t] += mapping[p, t];
                }
            }

            //Divide the value in the map with the summed up duration.
            foreach(int p in sortedPlanProjects.Keys)
            {
                for(int s = 0; s < totalDurationSums.Count; s++)
                {
                    if (mapping[p,s] != 0)
                    {
                        sortedPlanProjects[p].TogglProjectIds[sortedTogglProjects[s].TogglId] = mapping[p, s] / totalDurationSums[s];
                    }
                }
            }
        }
    }
}
