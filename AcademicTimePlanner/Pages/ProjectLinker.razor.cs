using AcademicTimePlanner.Store.State.ProjectLinker;
using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AcademicTimePlanner.Pages
{
    public partial class ProjectLinker
    {
        [Inject]
        private IState<ProjectLinkerState> ProjectLinkerState { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        private Data.Data? ChartData => ProjectLinkerState.Value.ChartData;

        private const string Title = "Link projects";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new SetTitleAction(Title));
            Dispatcher.Dispatch(new FetchChartDataAction());
        }

        private void LinkProjects()
        {

        }
    }
}