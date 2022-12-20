using AcademicTimePlanner.ApplicationData.Plan;
using AcademicTimePlanner.Store.State.ProjectLinker;
using AcademicTimePlanner.Store.State.Wrapper;
using AcademicTimePlanner.DisplayData;
using Fluxor;
using Microsoft.AspNetCore.Components;
using AcademicTimePlanner.UIModels;

namespace AcademicTimePlanner.Pages
{
    public partial class ProjectLinker
    {
        [Inject]
        private IState<ProjectLinkerState> LinkerState { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        private ProjectsData? ProjectsData => LinkerState.Value.ProjectsData;

        private ProjectSelector ProjectSelector => LinkerState.Value.ProjectSelector;

        private TaskSelector TaskSelector => LinkerState.Value.TaskSelector;

        private PlanProject? PlanProject => LinkerState.Value.PlanProject;

        private const string Title = "Link projects and tasks";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new SetTitleAction(Title));
            Dispatcher.Dispatch(new FetchProjectsDataAction());
        }

        protected void LinkProjectsClick(EventArgs e, bool link)
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

        private void SwitchLinkingStep(ProjectLinkerState.LinkingStep step)
        {
            if (step == ProjectLinkerState.LinkingStep.TaskLinking && !ProjectSelector.PlanProjectId.Equals(Guid.Empty) || step == ProjectLinkerState.LinkingStep.ProjectLinking)
            {
                var planProject = ProjectsData!.PlanProjects.Find(project => project.Id.Equals(ProjectSelector.PlanProjectId))!;
                Dispatcher.Dispatch(new SwitchLinkingStepAction(step, planProject));
            }
        }

        protected void LinkTasksClick(EventArgs e, bool link)
        {
            if (link)
                LinkTasks();
            else
                UnlinkTasks();
            Dispatcher.Dispatch(new SaveProjectsDataAction(ProjectsData!));
        }

        private void LinkTasks()
        {
            var planTask = PlanProject.PlanTasks.Find(task => task.Id.Equals(TaskSelector.PlanTaskId))!;

            if (!planTask.TogglIds.ContainsKey(TaskSelector.TogglTaskTogglId))
                planTask.TogglIds.Add(TaskSelector.TogglTaskTogglId, 1);
        }

        private void UnlinkTasks()
        {
            var planTask = PlanProject.PlanTasks.Find(task => task.Id.Equals(TaskSelector.PlanTaskId))!;

            if (planTask.TogglIds.ContainsKey(TaskSelector.TogglTaskTogglId))
                planTask.TogglIds.Remove(TaskSelector.TogglTaskTogglId);
        }
    }
}