﻿using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Services.DataManagerService
{
    /// <summary>
    /// Handles access to the <see cref="DataManager"/> of the current session.
    /// The <see cref="DataManager"/> can only be accessed via this service.
    /// </summary>
    public interface IDataManagerService
    {
        /// <summary>
        /// Updates the Toggl data hold by <see cref="DataManager"/>.
        /// </summary>
        /// <param name="togglProjects"></param>
        /// <returns></returns>
        public Task SetTogglProjects(List<TogglProject> togglProjects);

        /// <summary>
        /// Gets the Toggl data hold by <see cref="DataManager"/>.
        /// </summary>
        /// <returns></returns>
        public Task<List<TogglProject>> GetTogglProjects();

        /// <summary>
        /// Updates the planning data hold by <see cref="DataManager"/>
        /// </summary>
        /// <param name="planProjects"></param>
        /// <returns></returns>
        public Task SetPlanProjects(List<PlanProject> planProjects);

        /// <summary>
        /// Gets the planning data hold by <see cref="DataManager"/>.
        /// </summary>
        /// <returns></returns>
        public Task<List<PlanProject>> GetPlanProjects();

        /// <summary>
        /// Gets the current chart data from the <see cref="DataManager"/>
        /// </summary>
        /// <returns></returns>
        public Task<ChartData> GetChartData();
    }
}
