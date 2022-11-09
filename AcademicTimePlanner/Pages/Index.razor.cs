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
    private IState<ProjectFilesState> ProjectFilesState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    private ImmutableSortedSet<string> PlanProjectsNames => ProjectFilesState.Value.PlanProjectsNames;
    
    private const string Title = "Startseite";

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
}