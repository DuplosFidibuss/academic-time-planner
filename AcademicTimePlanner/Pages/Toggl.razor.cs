using AcademicTimePlanner.Data;
using AcademicTimePlanner.Store.State.Toggl;
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

	private TogglSettings TogglSettings { get; set; } = new();
    
    private int NumberOfTogglProjects => TogglState.Value.NumberOfTogglProjects;
    
    private const string Title = "Toggl";
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
    }
    
    private void SaveTogglSettings()
    {
        Dispatcher.Dispatch(new SaveTogglSettingsAction(TogglSettings));
    }
}