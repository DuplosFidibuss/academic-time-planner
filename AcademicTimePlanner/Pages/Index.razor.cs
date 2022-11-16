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

    private PlanProject planProject;

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

    /*public PlanProject(long togglProjectId, string name)
        {
            Id = Guid.NewGuid();
            TogglProjectId = togglProjectId;
            Name = name;
            Tasks = new Dictionary<long, string>();
            PlanEntries = new List<PlanEntry>();
            RepetitionEntries = new List<PlanEntryRepetition>();
        }
    */
    private void CreatePlanProject()
    {
        planProject = new();


    }
}