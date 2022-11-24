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
            TogglProjects.Clear();
            TestTogglProject.GetTestTogglProject().ForEach(project => TogglProjects.Add(project));
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

        //TODO figure out where to call this method
        public void UpdateTogglDictionaryInPlanProjects(List<PlanProject> planProjects, List<TogglProject> togglProjects)
        {
            SortedList<int, PlanProject> pProjects = new SortedList<int, PlanProject>();
            SortedList<int, TogglProject> tProjects = new SortedList<int, TogglProject>();

            for(int i = 0; i < togglProjects.Count; i++)
            {
                tProjects.Add(i, togglProjects[i]);
            }
            for(int i = 0; i < planProjects.Count; i++)
            {
                pProjects.Add(i, planProjects[i]);
            }

            double[,] mapping = new double[pProjects.Count, tProjects.Count];

            foreach(int p in pProjects.Keys)
            {
                double duration = pProjects[p].GetTotalDuration();
                foreach (int t in tProjects.Keys)
                {
                    if (pProjects[p].TogglProjectIds.ContainsKey(tProjects[t].TogglId))
                        mapping[p,t] = duration;
                }
            }

            List<double> sum = new List<double>();

            foreach(int t in tProjects.Keys)
            {
                sum.Add(0);
                foreach(int p in pProjects.Keys)
                {
                    sum[t] += mapping[p, t];
                }
            }

            foreach(int p in pProjects.Keys)
            {
                for(int s = 0; s < sum.Count; s++)
                {
                    if (mapping[p,s] != 0)
                    {
                        pProjects[p].TogglProjectIds[tProjects[s].TogglId] = mapping[p, s] / sum[s];
                    }
                }
            }
        }
    }
}
