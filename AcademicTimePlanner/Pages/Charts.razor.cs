using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor;
using Bar = Plotly.Blazor.Traces.Bar;

namespace AcademicTimePlanner.Pages;

public partial class Charts
{
    [Inject]
    private IDispatcher Dispatcher { get; set; }
    
    private const string Title = "Graphen";
    
    PlotlyChart chart;

    Config config = new Config
    {
        Responsive = true
    };

    Layout layout = new Layout
    {
        Title = new Title
        {
            Text = "Bar"
        },
        BarMode = BarModeEnum.Stack,
        Height = 500
    };

    List<ITrace> data = new List<ITrace>
    {
        new Bar
        {
            X = new List<object> {"giraffes", "orangutans", "monkeys"},
            Y = new List<object> {20, 14, 23},
            Name = "SF Zoo"
        },
        new Bar
        {
            X = new List<object> {"giraffes", "orangutans", "monkeys"},
            Y = new List<object> {12, 18, 29},
            Name = "LA Zoo"
        }
    };

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
    }
}