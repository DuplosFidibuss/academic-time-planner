using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor;
using Bar = Plotly.Blazor.Traces.Bar;
using Scatter = Plotly.Blazor.Traces.Scatter;
using Marker = Plotly.Blazor.Traces.BarLib.Marker;
using Plotly.Blazor.Traces.ScatterLib;
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
    private DateFilter? DateFilter => ChartsState.Value.DateFilter;
    
    private const string Title = "Graphen";
    private const string TotalChartTitle = "Total";

    private static readonly Marker TrackedDurationMarker = new Marker { Color = "rgb(20, 150, 70)" };
    private static readonly Marker PlannedDurationMarker = new Marker { Color = "rgb(20, 70, 150)" };
    private static readonly Marker PredictedDurationMarker = new Marker { Color = "rgb(34, 220, 93)" };
    private static readonly Marker TotalDurationMarker = new Marker { Color = "rgb(34, 120, 250)" };

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
        Width = 700,
    };

    Layout layoutProjects = new Layout
    {
        Title = new Title
        {
            Text = "Projects overview"
        },
        BarMode = BarModeEnum.Group,
        XAxis = new List<XAxis> { new XAxis { Anchor="free", Position=0, TickAngle=45 }, new XAxis { Anchor="free", Position=0, Overlaying="x", TickAngle=45 } },
        Height = 500,
        AutoSize = true,
        BarGroupGap = 0
    };

    //TODO change to linediagram
    private Layout GetLayout()
    {
        return new Layout
        {
            Title = new Title
            {
                Text = "Project overview in time range (" + DateFilter.StartDate.Date + " - " + DateFilter.EndDate.Date + ")"
            },

            Height = 500,
            AutoSize = true,
        };
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
                Marker = PredictedDurationMarker,
            },
            new Bar
            {
                X = new List<object> {TotalChartTitle},
                Y = new List<object> {ChartData!.TotalPlannedTime},
                Name = "Total planned",
                XAxis = "x2",
                Marker = PlannedDurationMarker,

            },
            new Bar
            {
                X = new List<object> {TotalChartTitle},
                Y = new List<object> {ChartData!.TotalTrackedTime},
                Name = "Total tracked",
                XAxis = "x2",
                Marker = TrackedDurationMarker,
            },
        };    
    }

    private List<ITrace> GetDataOfSingleProjectsToday()
    {
        var titles = new List<object>();
        var totalDurations = new List<object>();
        var predictedDurations = new List<object>();
        var plannedDurations = new List<object>();
        var trackedDurations = new List<object>();

        foreach (var planProject in ChartData!.PlanProjects)
        {
            var togglProject = ChartData!.GetTogglProjectWithTogglId(planProject.TogglProjectId);
            titles.Add(planProject.Name);

            totalDurations.Add(planProject.GetTotalDuration());
            predictedDurations.Add(togglProject.GetTotalDuration() + planProject.GetRemainingDuration());
            plannedDurations.Add(planProject.GetTotalDuration() - planProject.GetRemainingDuration());
            trackedDurations.Add(togglProject.GetTotalDuration());
        }

        return new List<ITrace>
        {
            new Bar
            {
                X = titles,
                Y = totalDurations,
                Name = "Predicted",
                Marker = TotalDurationMarker,
            },
            new Bar
            {
                X = titles,
                Y = predictedDurations,
                Name = "Predicted",
                Marker = PredictedDurationMarker,
            },
            new Bar
            {
                X = titles,
                Y = plannedDurations,
                Name = "Planned",
                XAxis = "x2",
                Marker = PlannedDurationMarker,
            },
            new Bar
            {
                X = titles,
                Y = trackedDurations,
                Name = "Tracked",
                XAxis = "x2",
                Marker = TrackedDurationMarker,
            },
        };
    }

    private List<ITrace> GetDataOfSingleProjectsFiltered(PlanProject planProject)
    {
        var togglProject = ChartData!.GetTogglProjectWithTogglId(planProject.TogglProjectId);
        var titles = new List<object>();
        var plannedDurations = planProject.GetDurationDictionaryInTimeRange(DateFilter.StartDate, DateFilter.EndDate);
        var trackedDurations = togglProject.GetDurationDictionaryInTimeRange(DateFilter.StartDate, DateFilter.EndDate);

        titles.Add(planProject.Name);

        return new List<ITrace>
        {
            new Scatter
            {
                X = (IList<object>)plannedDurations.Keys.ToList(),
                Y = (IList<object>)plannedDurations.Values.ToList(),
                Name = "Planned",
                Mode = ModeFlag.Lines| ModeFlag.Markers,
                //Marker = PlannedDurationMarker,
            },
            new Scatter
            {
                X = (IList<object>)trackedDurations.Keys.ToList(),
                Y = (IList<object>)trackedDurations.Values.ToList(),
                Name = "Tracked",
                Mode = ModeFlag.Lines| ModeFlag.Markers,
                //Marker = TrackedDurationMarker,
            },
        };
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new SetTitleAction(Title));
        Dispatcher.Dispatch(new FetchChartDataAction());
    }

    private void SetDateFilter()
    {
        Dispatcher.Dispatch(new FilterChartDataAction());
    }

    private void ChangeFilter()
    {
        Dispatcher.Dispatch(new ChangeFilterAction());
    }
}