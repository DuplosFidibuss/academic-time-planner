using AcademicTimePlanner.Data;
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

        public async Task<bool> SetTogglProjects(List<TogglProject> togglProjects)
        {
            var dataManager = await _localStorage.GetItemAsync<DataManager>(nameof(DataManager));
            var isInitialized = await _localStorage.GetItemAsync<bool>("Initialized");
            if (dataManager == null)
            {
                if (!isInitialized)
                {
                    dataManager = new DataManager();
                    await _localStorage.SetItemAsync("Initialized", true);
                }
                else
                {
                    return false;
                }
            }

            await _localStorage.RemoveItemAsync(nameof(DataManager));
            dataManager.TogglProjects = togglProjects;
            await _localStorage.SetItemAsync(nameof(DataManager), dataManager);
            return true;
        }
    }
}
