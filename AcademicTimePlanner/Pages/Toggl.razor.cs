using AcademicTimePlanner.Store.State.Toggl;
using AcademicTimePlanner.Store.State.Wrapper;
using AcademicTimePlanner.DisplayData;
using Fluxor;
using Microsoft.AspNetCore.Components;
using System.Collections.Immutable;
using AcademicTimePlanner.UIModels;

namespace AcademicTimePlanner.Pages;

public partial class Toggl
{
    [Inject]
    private IState<TogglState> TogglState { get; set; }
    
    [Inject]
    private IDispatcher Dispatcher { get; set; }

	private TogglSettings TogglSettings { get; set; } = new();
    
    private List<TogglLoadOverviewData> LoadOverview => TogglState.Value.LoadOverview;
	private DateTime LastSynchronized => TogglState.Value.LastSynchronized;
    
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

	private void Synchronize()
	{
		Dispatcher.Dispatch(new FetchTogglDataAction());
	}
}