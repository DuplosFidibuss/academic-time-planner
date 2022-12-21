using AcademicTimePlanner.ApplicationData.Plan;
using AcademicTimePlanner.DisplayData;
using AcademicTimePlanner.Store.State.ProjectLinker;
using AcademicTimePlanner.Store.State.Wrapper;
using AcademicTimePlanner.UIModels;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AcademicTimePlanner.Pages
{
    public partial class ProjectLinker
    {
        private const string Title = "Link projects and tasks";

        [Inject]
        private IState<ProjectLinkerState> _linkerState { get; set; }

        [Inject]
        private IDispatcher _dispatcher { get; set; }

        private ProjectsData? _projectsData => _linkerState.Value.ProjectsData;

        private ProjectSelector _projectSelector => _linkerState.Value.ProjectSelector;

        private TaskSelector _taskSelector => _linkerState.Value.TaskSelector;

        private PlanProject? _planProject => _linkerState.Value.PlanProject;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _dispatcher.Dispatch(new SetTitleAction(Title));
            _dispatcher.Dispatch(new FetchProjectsDataAction());
        }

        protected void LinkProjectsClick(EventArgs e, bool link)
        {
            if (link)
                LinkProjects();
            else
                UnlinkProjects();
            _dispatcher.Dispatch(new SaveProjectsDataAction(_projectsData!));
        }

        private void LinkProjects()
        {
            var planProject = _projectsData!.PlanProjects.Find(project => project.Id.Equals(_projectSelector.PlanProjectId))!;

            if (!planProject.TogglProjectIds.ContainsKey(_projectSelector.TogglProjectTogglId))
                planProject.TogglProjectIds.Add(_projectSelector.TogglProjectTogglId, 1);
        }

        private void UnlinkProjects()
        {
            var planProject = _projectsData!.PlanProjects.Find(project => project.Id.Equals(_projectSelector.PlanProjectId))!;

            if (planProject.TogglProjectIds.ContainsKey(_projectSelector.TogglProjectTogglId))
                planProject.TogglProjectIds.Remove(_projectSelector.TogglProjectTogglId);
        }

        private void SwitchLinkingStep(ProjectLinkerState.LinkingStep step)
        {
            if (step == ProjectLinkerState.LinkingStep.TaskLinking && !_projectSelector.PlanProjectId.Equals(Guid.Empty) || step == ProjectLinkerState.LinkingStep.ProjectLinking)
            {
                var planProject = _projectsData!.PlanProjects.Find(project => project.Id.Equals(_projectSelector.PlanProjectId))!;
                _dispatcher.Dispatch(new SwitchLinkingStepAction(step, planProject));
            }
        }

        protected void LinkTasksClick(EventArgs e, bool link)
        {
            if (link)
                LinkTasks();
            else
                UnlinkTasks();
            _dispatcher.Dispatch(new SaveProjectsDataAction(_projectsData!));
        }

        private void LinkTasks()
        {
            var planTask = _planProject!.PlanTasks.Find(task => task.Id.Equals(_taskSelector.PlanTaskId))!;

            if (!planTask.TogglIds.ContainsKey(_taskSelector.TogglTaskTogglId))
                planTask.TogglIds.Add(_taskSelector.TogglTaskTogglId, 1);
        }

        private void UnlinkTasks()
        {
            var planTask = _planProject!.PlanTasks.Find(task => task.Id.Equals(_taskSelector.PlanTaskId))!;

            if (planTask.TogglIds.ContainsKey(_taskSelector.TogglTaskTogglId))
                planTask.TogglIds.Remove(_taskSelector.TogglTaskTogglId);
        }
    }
}