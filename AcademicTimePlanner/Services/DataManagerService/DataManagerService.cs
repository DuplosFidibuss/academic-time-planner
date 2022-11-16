using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;
using Blazored.LocalStorage;

namespace AcademicTimePlanner.Services.DataManagerService
{
    public class DataManagerService : IDataManagerService
    {
        private readonly ILocalStorageService _localStorage;

        public DataManagerService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task UpdateTogglProjects(List<TogglProject> togglProjects)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
                dataManager = new DataManager();
            dataManager.UpdateTogglData(togglProjects);
            await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
        }

        public async Task SetPlanProjects(List<PlanProject> planProjects)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
                dataManager = new DataManager();
            dataManager.PlanProjects = planProjects;
            await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
        }

        public async Task<ChartData> GetChartData()
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
            {
                dataManager = new DataManager();
                await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
            }
            return dataManager.GetChartData();
        }

        public async Task AddPlanProject(PlanProject planProject)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
            {
                dataManager = new DataManager();
            }
            dataManager.PlanProjects.Add(planProject);
            await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
        }

        public async Task<List<TogglLoadOverviewData>> GetTogglLoadOverview()
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
            {
                dataManager = new DataManager();
                await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
            }
            return dataManager.GetTogglLoadOverview();
        }
    }
}
