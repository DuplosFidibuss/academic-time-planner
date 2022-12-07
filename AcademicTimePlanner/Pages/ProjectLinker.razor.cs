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

        protected void OnClick(EventArgs e, bool link)
        {
            if (link)
                LinkProjects();
            else
                UnlinkProjects();
            Dispatcher.Dispatch(new SaveProjectsDataAction(ProjectsData!));
        }

        private void LinkProjects()
        {
            var planProject = ProjectsData!.PlanProjects.Find(project => project.Id.Equals(ProjectSelector.PlanProjectId))!;

            if (!planProject.TogglProjectIds.ContainsKey(ProjectSelector.TogglProjectTogglId))
                planProject.TogglProjectIds.Add(ProjectSelector.TogglProjectTogglId, 1);
        }

        private void UnlinkProjects()
        {
            var planProject = ProjectsData!.PlanProjects.Find(project => project.Id.Equals(ProjectSelector.PlanProjectId))!;

            if (planProject.TogglProjectIds.ContainsKey(ProjectSelector.TogglProjectTogglId))
                planProject.TogglProjectIds.Remove(ProjectSelector.TogglProjectTogglId);
        }
    }
}