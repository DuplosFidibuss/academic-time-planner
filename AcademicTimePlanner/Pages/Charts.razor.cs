using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor;
using Bar = Plotly.Blazor.Traces.Bar;
using AcademicTimePlanner.Store.State.Charts;
using AcademicTimePlanner.Data;

namespace AcademicTimePlanner.Pages;

public partial class Charts
{
    [Inject]
    private IState<ChartsState> ChartsState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    private ChartData? ChartData => ChartsState.Value.ChartData;
    
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
            Text = "Total overview"
        },
        BarMode = BarModeEnum.Group,
        Height = 500
    };

    private List<ITrace> GetData()
    {
        return new List<ITrace>
        {
            new Bar
            {
                X = new List<object> {"Total planned", "Total tracked"},
                Y = new List<object> {ChartData!.TotalPlannedTime, ChartData!.TotalTrackedTime},
                Name = "Total",
            },
        };    
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
    }

    private void LoadChartData()
    {
        Dispatcher.Dispatch(new FetchChartDataAction());
    }
}