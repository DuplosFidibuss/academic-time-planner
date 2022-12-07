using AcademicTimePlanner.Data;
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

        private DisplayData? ProjectsData => ProjectLinkerState.Value.ProjectsData;

        private ProjectSelector ProjectSelector => ProjectLinkerState.Value.ProjectSelector;

        private const string Title = "Link projects";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new SetTitleAction(Title));
            Dispatcher.Dispatch(new FetchProjectsDataAction());
        }

        private void LinkProjects()
        {

        }
    }
}