using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.Store.State.ProjectFiles;
using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AcademicTimePlanner.Pages;

public partial class Index
{
    [Inject]
    private IState<ProjectFilesState> ProjectState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    private List<string> PlanProjectsNames => ProjectState.Value.PlanProjectsNames;
    
    private const string Title = "Startseite";

    private PlanProject? planProject => ProjectState.Value.PlanProject;

    private PlanTask? planTask => ProjectState.Value.PlanTask;

    private PlanEntry? planEntry => ProjectState.Value.PlanEntry;

    private PlanEntryRepetition? planEntryRepetition => ProjectState.Value.PlanEntryRepetition;

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
    }

    private void CreatePlanEntry()
    {
        planProject.AddPlanEntry(planEntry);
        Dispatcher.Dispatch(new AddSingleEntryAction());
    }

    private void CreateRepetitionEntry()
    {
        planProject.AddRepetitionEntry(planEntryRepetition);
        Dispatcher.Dispatch(new AddRepetitionEntryAction());
    }

    private void AddSingleEntry()
    {
        Dispatcher.Dispatch(new AddSingleEntryAction());
    }

    private void AddRepetitionEntry()
    {
        Dispatcher.Dispatch(new AddRepetitionEntryAction());
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