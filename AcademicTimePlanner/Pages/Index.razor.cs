using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.Store.State.ProjectFiles;
using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Immutable;

namespace AcademicTimePlanner.Pages;

public partial class Index
{
    [Inject]
    private IState<ProjectFilesState> ProjectState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    private ImmutableSortedSet<string> PlanProjectsNames => ProjectState.Value.PlanProjectsNames;
    
    private const string Title = "Startseite";

    private PlanProject? planProject => ProjectState.Value.PlanProject;

    private PlanTask? planTask = new PlanTask(Guid.NewGuid());

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
    }

    private async Task LoadPlanProjects(InputFileChangeEventArgs e)
    {
        var json = new List<string>();
        foreach (var file in e.GetMultipleFiles(int.MaxValue))
        {
            json.Add(await new StreamReader(file.OpenReadStream()).ReadToEndAsync());
        }
        Dispatcher.Dispatch(new LoadPlanProjectsAction(json));
    }

    private void CreatePlanProject()
    {
        Dispatcher.Dispatch(new SwitchCreationStepAction(ProjectFilesState.CreationStep.NamingProject, new PlanProject(Guid.NewGuid())));
    }

    private void CreatePlanTask()
    {
        Dispatcher.Dispatch(new CreatePlanTaskAction(planTask));
        planTask = new PlanTask(Guid.NewGuid());
    }

    private void Cancel()
    {
        Dispatcher.Dispatch(new SwitchCreationStepAction(ProjectFilesState.CreationStep.NotCreating, null));
    }

    private void NextOrBack(ProjectFilesState.CreationStep step)
    {
        Dispatcher.Dispatch(new SwitchCreationStepAction(step, planProject!));
    }

    private void Finish()
    {
        Dispatcher.Dispatch(new FinishPlanProjectCreationAction(planProject!));
    }
}