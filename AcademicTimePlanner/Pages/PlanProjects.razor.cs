using AcademicTimePlanner.ApplicationData.Plan;
using AcademicTimePlanner.Store.State.ProjectFiles;
using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace AcademicTimePlanner.Pages;

public partial class PlanProjects
{
    [Inject]
    private IState<ProjectFilesState> ProjectState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    private List<PlanProject> Projects => ProjectState.Value.PlanProjects;

    private const string Title = "Plan projects";

    private PlanProject? PlanProject => ProjectState.Value.PlanProject;

    private PlanTask? PlanTask => ProjectState.Value.PlanTask;

    private PlanEntry? PlanEntry => ProjectState.Value.PlanEntry;

    private PlanEntryRepetition? PlanEntryRepetition => ProjectState.Value.PlanEntryRepetition;

    private string OldPlanProjectName;
    private List<PlanTask> DeletedPlanTaks = new List<PlanTask>();
    private List<PlanEntry> DeletedPlanEntries = new List<PlanEntry>();
    private List<PlanEntryRepetition> DeletedPlanEntryRepetitions = new List<PlanEntryRepetition>();
    private List<PlanTask> AddedPlanTasks = new List<PlanTask>();
    private List<PlanEntry> AddedPlanEntries = new List<PlanEntry>();
    private List<PlanEntryRepetition> AddedPlanEntryRepetitions = new List<PlanEntryRepetition>();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
        Dispatcher.Dispatch(new FetchPlanProjectsAction());
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
        if (PlanTask.IsValidPlanTask())
        {
            AddedPlanTasks.Add(PlanTask);
            Dispatcher.Dispatch(new CreatePlanTaskAction(PlanTask));
        }
    }

    private void CreatePlanEntry()
    {
        if (PlanEntry.IsValidPlanEntry())
        {
            AddedPlanEntries.Add(PlanEntry);
            PlanProject.AddPlanEntry(PlanEntry);
            Dispatcher.Dispatch(new AddSingleEntryAction());
        }
    }

    private void CreateRepetitionEntry()
    {
        if (PlanEntryRepetition.IsValidPlanEntryRepetition())
        {
            AddedPlanEntryRepetitions.Add(PlanEntryRepetition);
            PlanEntryRepetition.Modify();
            PlanProject.AddRepetitionEntry(PlanEntryRepetition);
            Dispatcher.Dispatch(new AddRepetitionEntryAction());
        }
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
        RevertChangesOnProject();
        Dispatcher.Dispatch(new SwitchCreationStepAction(ProjectFilesState.CreationStep.NotCreating, null));
    }

    private void RevertChangesOnProject()
    {
        PlanProject!.Name = OldPlanProjectName;
        DeletedPlanTaks.ForEach(task => PlanProject!.AddPlanTask(task));
        DeletedPlanEntries.ForEach(entry => PlanProject!.AddPlanEntry(entry));
        DeletedPlanEntryRepetitions.ForEach(entry => PlanProject!.AddRepetitionEntry(entry));
        AddedPlanTasks.ForEach(task => PlanProject!.RemovePlanTask(task));
        AddedPlanEntries.ForEach(entry => PlanProject!.RemovePlanEntry(entry));
        AddedPlanEntryRepetitions.ForEach(entry => PlanProject!.RemoveRepetitionEntry(entry));
    }

    private void NextOrBack(ProjectFilesState.CreationStep step)
    {
        Dispatcher.Dispatch(new SwitchCreationStepAction(step, PlanProject!));
    }

    private void Finish()
    {
        if (!string.IsNullOrWhiteSpace(PlanProject!.Name))
            Dispatcher.Dispatch(new FinishPlanProjectCreationAction(PlanProject!));
    }

    private async Task DownloadPlanProject(PlanProject planProject)
    {
        var fileStream = new MemoryStream();
        var writer = new StreamWriter(fileStream);
        writer.Write(JsonConvert.SerializeObject(planProject, Formatting.Indented));
        writer.Flush();
        fileStream.Position = 0;
        using var streamRef = new DotNetStreamReference(stream: fileStream);
        await JSRuntime.InvokeVoidAsync("downloadFileFromStream", planProject!.Name + ".json", streamRef);
    }

    private void DeletePlanProject(EventArgs e, Guid planProjectId)
    {
        Dispatcher.Dispatch(new DeletePlanProjectAction(planProjectId));
    }

    private void EditPlanProject(EventArgs e, PlanProject planProject)
    {
        OldPlanProjectName = planProject.Name;
        Dispatcher.Dispatch(new SwitchCreationStepAction(ProjectFilesState.CreationStep.NamingProject, planProject));
    }

    private void DeletePlanTask(EventArgs e, PlanTask planTask)
    {
        PlanProject!.RemovePlanTask(planTask);
        DeletedPlanTaks.Add(planTask);
    }

    private void DeletePlanEntry(EventArgs e, PlanEntry planEntry)
    {
        PlanProject!.RemovePlanEntry(planEntry);
        DeletedPlanEntries.Add(planEntry);
    }

    private void DeletePlanEntryRepetition(EventArgs e, PlanEntryRepetition entryRepetition)
    {
        PlanProject!.RemoveRepetitionEntry(entryRepetition);
        DeletedPlanEntryRepetitions.Add(entryRepetition);
    }
}