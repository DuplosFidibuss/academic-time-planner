using AcademicTimePlanner.Data.DisplayData;
using AcademicTimePlanner.Data.MetaData;
using AcademicTimePlanner.Store.State.Toggl;
using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AcademicTimePlanner.Pages
{
    public partial class Toggl
    {
        private const string Title = "Toggl";

        [Inject]
        private IState<TogglState> _togglState { get; set; }

        [Inject]
        private IDispatcher _dispatcher { get; set; }

        private TogglSettings _togglSettings { get; set; } = new();

        private List<TogglLoadOverviewData> _loadOverview => _togglState.Value.LoadOverview;

        private DateTime _lastSynchronized => _togglState.Value.LastSynchronized;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _dispatcher.Dispatch(new SetTitleAction(Title));
            Synchronize();
        }

        private void SaveTogglSettings()
        {
            _dispatcher.Dispatch(new SaveTogglSettingsAction(_togglSettings));
        }

        private void Synchronize()
        {
            _dispatcher.Dispatch(new FetchTogglDataAction());
        }
    }
}