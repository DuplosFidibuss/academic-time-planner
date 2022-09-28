using AcademicTimePlanner.Store.State.Editor;
using AcademicTimePlanner.Store.State.Toggl;
using AcademicTimePlanner.Store.State.TogglSettings;
using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AcademicTimePlanner.Pages;

public partial class Toggl
{
    [Inject]
    private IState<TogglState> TogglState { get; set; }
    
    [Inject]
    private IDispatcher Dispatcher { get; set; }

    private string TogglApiKey;
    private string TogglWorkspaceKey;
    
    private List<(double Number, string Label)> TogglChartData => TogglState.Value.TogglEntrySums
        .Select(item => ((double) item.Duration, item.Date.ToString("yyyy-MM-dd"))).ToList();
    
    private const string Title = "Toggl";
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
    }

    private void AskForTogglCredentials()
    {
        Dispatcher.Dispatch(new EditorAskForTogglCredentialsAction());
    }
    
    public void SaveTogglSettings()
    {
        Dispatcher.Dispatch(new SetTogglSettingsAction(TogglApiKey, TogglWorkspaceKey));
    }
}