using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AcademicTimePlanner.Pages;

public partial class PlanGraph
{
    [Inject]
    private IDispatcher Dispatcher { get; set; }

    private const string Title = "Plan Graph";
    private List<(double Number, string Label)> _planGraphChartData = new()
    {
        (10, "Balken1"),
        (20, "Balken2"),
        (50, "Balken3")
    };
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
    }
}