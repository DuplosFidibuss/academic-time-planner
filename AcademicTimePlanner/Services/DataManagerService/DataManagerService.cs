using AcademicTimePlanner.DataManagement;
using AcademicTimePlanner.ApplicationData.Plan;
using AcademicTimePlanner.ApplicationData.Toggl;
using AcademicTimePlanner.DisplayData;
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

        public async Task UpdatePlanProjects(List<PlanProject> planProjects)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
                dataManager = new DataManager();
            dataManager.UpdatePlanningData(planProjects);
            await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
        }

        public async Task<ProjectsData> GetProjectsData()
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
            {
                dataManager = new DataManager();
                await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
            }
            return dataManager.GetProjectsData();
        }

        public async Task UpdatePlanProject(PlanProject planProject)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
            {
                dataManager = new DataManager();
            }
            dataManager.UpdatePlanProject(planProject);
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

        public async Task<List<PlanProject>> GetPlanProjects()
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
            {
                dataManager = new DataManager();
                await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
            }
            return new List<PlanProject>(dataManager.PlanProjects);
        }

        public async Task<PlanProject> GetPlanProjectById(Guid id)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
            {
                dataManager = new DataManager();
                await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
            }
            return dataManager.PlanProjects.Find(planProject => planProject.Id == id)!;
        }

        public async Task DeletePlanProject(Guid planProjectId)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
            {
                dataManager = new DataManager();
            }
            dataManager.DeletePlanProject(planProjectId);
            await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
        }
    }
}
