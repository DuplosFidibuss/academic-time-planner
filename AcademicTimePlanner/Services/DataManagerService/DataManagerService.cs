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

        public async Task SetTogglProjects(List<TogglProject> togglProjects)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            if (dataManager == null)
                dataManager = new DataManager();
            dataManager.TogglProjects = togglProjects;
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
    }
}
