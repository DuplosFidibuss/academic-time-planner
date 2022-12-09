using AcademicTimePlanner.DataMapping.Plan;
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

    private PlanProject? planProject => ProjectState.Value.PlanProject;

    private PlanTask? planTask => ProjectState.Value.PlanTask;

    private PlanEntry? planEntry => ProjectState.Value.PlanEntry;

    private PlanEntryRepetition? planEntryRepetition => ProjectState.Value.PlanEntryRepetition;

    private PlanProjectDownloader downloader => ProjectState.Value.PlanProjectDownloader;

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
        if (planTask.IsValidPlanTask())
            Dispatcher.Dispatch(new CreatePlanTaskAction(planTask));
    }

    private void CreatePlanEntry()
    {
        if (planEntry.IsValidPlanEntry())
        {
            planProject.AddPlanEntry(planEntry);
            Dispatcher.Dispatch(new AddSingleEntryAction());
        }
    }

    private void CreateRepetitionEntry()
    {
        if (planEntryRepetition.IsValidPlanEntryRepetition())
        {
            planEntryRepetition.Modify();
            planProject.AddRepetitionEntry(planEntryRepetition);
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
        Dispatcher.Dispatch(new SwitchCreationStepAction(ProjectFilesState.CreationStep.NotCreating, null));
    }

    private void NextOrBack(ProjectFilesState.CreationStep step)
    {
        Dispatcher.Dispatch(new SwitchCreationStepAction(step, planProject!));
    }

    private void Finish()
    {
        if (!string.IsNullOrWhiteSpace(planProject!.Name))
            Dispatcher.Dispatch(new FinishPlanProjectCreationAction(planProject!));
    }

    private void InitializePlanProjectDownload()
    {
        Dispatcher.Dispatch(new GetPlanProjectForDownloadAction(downloader.ProjectName));
    }

    private async Task DownloadPlanProject()
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
}