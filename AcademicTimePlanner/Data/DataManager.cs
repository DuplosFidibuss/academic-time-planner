﻿using AcademicTimePlanner.DataMapping.Budget;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class DataManager
    {
        /// <summary>
        /// This class holds all data used and created by the application.
        /// </summary>
        public DataManager()
        {
            Budgets = new List<Budget>();
            PlanProjects = new List<PlanProject>();
            TogglProjects = new List<TogglProject>() { TestTogglProject.GetTestTogglProject() };
        }

        public List<Budget> Budgets { get; set; }
        public List<PlanProject> PlanProjects { get; set; }
        public List<TogglProject> TogglProjects { get; set; }

        public ChartData GetChartData()
        {
            return new ChartData(TogglProjects, PlanProjects);
        }
    }
}
