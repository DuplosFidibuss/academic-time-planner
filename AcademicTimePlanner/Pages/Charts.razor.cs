using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor;
using Bar = Plotly.Blazor.Traces.Bar;
using AcademicTimePlanner.Store.State.Charts;
using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Plan;

namespace AcademicTimePlanner.Pages;

public partial class Charts
{
    [Inject]
    private IState<ChartsState> ChartsState { get; set; }

    [Inject]
    private IDispatcher Dispatcher { get; set; }

    private ChartData? ChartData => ChartsState.Value.ChartData;
    
    private const string Title = "Graphen";
    private const string TotalChartTitle = "Total";
    
    PlotlyChart chart;

    Config config = new Config
    {
        Responsive = true
    };

    Layout layoutTotal = new Layout
    {
        Title = new Title
        {
            Text = "Total overview"
        },
        BarMode = BarModeEnum.Group,
        XAxis = new List<XAxis> { new XAxis { Anchor="free", Position=0 }, new XAxis { Anchor="free", Position=0, Overlaying="x" } },
        Height = 500,
        Width = 500,
    };

    Layout layoutProjects = new Layout
    {
        Title = new Title
        {
            Text = "Projects overview"
        },
        BarMode = BarModeEnum.Group,
        XAxis = new List<XAxis> { new XAxis { Anchor="free", Position=0 }, new XAxis { Anchor="free", Position=0, Overlaying="x" } },
        Height = 500,
        Width = 700,
    };

    private List<ITrace> GetDataOfSingleProjectsToday()
    {
        var data = new List<ITrace>();
        foreach (PlanProject planProject in ChartData!.PlanProjects)
        {
            var title = planProject.Name;
            var togglProject = ChartData.GetTogglProjectWithTogglId(planProject.TogglProjectId);
            var projectData = new List<ITrace>
            {
                new Bar
                {
                    X = new List<object> {title},
                    Y = new List<object> {planProject.GetTotalDuration()},
                    Name = "Predicted"
                },
                new Bar
                {
                    X = new List<object> {title},
                    Y = new List<object> {togglProject!.GetTotalDuration() + planProject.GetRemainingDuration()},
                    Name = "Predicted"
                },
                new Bar
                {
                    X = new List<object> {title},
                    Y = new List<object> {planProject.GetTotalDuration() - planProject.GetRemainingDuration()},
                    Name = "Planned",
                    XAxis = "x2",

                },
                new Bar
                {
                    X = new List<object> {title},
                    Y = new List<object> {togglProject!.GetTotalDuration()},
                    Name = "Tracked",
                    XAxis = "x2",

                },
            };
            data.AddRange(projectData);
        }
        return data;
    }

    private List<ITrace> GetDataTotal()
    {
        return new List<ITrace>
        {
            new Bar
            {
                X = new List<object> {TotalChartTitle},
                Y = new List<object> {ChartData!.TotalTrackedTime + ChartData!.RemainingDuration},
                Name = "Total predicted",
            },
            new Bar
            {
                X = new List<object> {TotalChartTitle},
                Y = new List<object> {ChartData!.TotalPlannedTime},
                Name = "Total planned",
                XAxis = "x2",

            },
            new Bar
            {
                X = new List<object> {TotalChartTitle},
                Y = new List<object> {ChartData!.TotalTrackedTime},
                Name = "Total tracked",
                XAxis = "x2",

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