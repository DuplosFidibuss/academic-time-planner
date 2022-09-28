using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AcademicTimePlanner.Pages;

public partial class Charts
{
    [Inject]
    private IDispatcher Dispatcher { get; set; }
    
    private const string Title = "Graphen";
    private List<(double Number, string Label)> _barChartData = new()
    {
        (10, "Balken1"),
        (20, "Balken2"),
        (50, "Balken3")
    };
    
    private List<(double Number, string Label)> _pieChartData = new()
    {
        (10, "Stück1"),
        (20, "Stück2"),
        (50, "Stück3")
    };
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
    }
}