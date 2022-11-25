using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor;
using YAxisTitle = Plotly.Blazor.LayoutLib.YAxisLib.Title;
using Bar = Plotly.Blazor.Traces.Bar;
using Scatter = Plotly.Blazor.Traces.Scatter;
using BarMarker = Plotly.Blazor.Traces.BarLib.Marker;
using LineMarker = Plotly.Blazor.Traces.ScatterLib.Marker;
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

    private static readonly BarMarker TrackedDurationMarker = new BarMarker { Color = "rgb(20, 150, 70)" };
    private static readonly BarMarker PlannedDurationMarker = new BarMarker { Color = "rgb(20, 70, 150)" };
    private static readonly BarMarker PredictedDurationMarker = new BarMarker { Color = "rgb(34, 220, 93)" };
    private static readonly BarMarker TotalDurationMarker = new BarMarker { Color = "rgb(34, 120, 250)" };

    private static readonly LineMarker TrackedDurationLine = new LineMarker { Color = "rgb(20, 150, 70)" };
    private static readonly LineMarker PlannedDurationLine = new LineMarker { Color = "rgb(20, 70, 150)" };

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
        YAxis = new List<YAxis> { new YAxis { Title = new YAxisTitle { Text = "hours" } } },
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
        YAxis = new List<YAxis> { new YAxis { Title = new YAxisTitle { Text = "hours" } } },
        Height = 500,
        AutoSize = true,
        BarGroupGap = 0
    };

    private Layout GetLayout(string projectName)
    {
        return new Layout
        {
            Title = new Title
            {
                Text = projectName + " overview in time range (" + DateFilter.StartDate.Date + " - " + DateFilter.EndDate.Date + ")"
            },
            YAxis = new List<YAxis> { new YAxis { Title = new YAxisTitle { Text = "hours" } } },
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
            double predictedDurationsSum = 0;
            double trackedDurationsSum = 0;
            foreach (long togglP in planProject.TogglProjectIds.Keys)
            {
                var togglProject = ChartData!.GetTogglProjectWithTogglId(togglP);
                predictedDurationsSum += (togglProject.GetTotalDuration() * planProject.TogglProjectIds[togglP] + planProject.GetRemainingDuration());
                trackedDurationsSum += (togglProject.GetTotalDuration() * planProject.TogglProjectIds[togglP]);
            }
            titles.Add(planProject.Name);

            totalDurations.Add(planProject.GetTotalDuration());
            predictedDurations.Add(predictedDurationsSum);
            plannedDurations.Add(planProject.GetTotalDuration() - planProject.GetRemainingDuration());
            trackedDurations.Add(trackedDurationsSum);
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
        var plannedDurations = planProject.GetDurationsPerDateInTimeRange(DateFilter.StartDate, DateFilter.EndDate);

		var plannedDurationsDates = new List<object>();
		var plannedDurationsTimes = new List<object>();
		plannedDurations.Keys.ToList().ForEach(date => plannedDurationsDates.Add(date));
		plannedDurations.Values.ToList().ForEach(time => plannedDurationsTimes.Add(time));

        var trackedDurationsDates = new List<object>();
        var trackedDurationsTimes = new List<object>();
        SortedDictionary<DateTime, double> trackedDurations = new SortedDictionary<DateTime, double>();
        int i = 0;

        foreach (long togglP in planProject.TogglProjectIds.Keys)
        {
            i += 1;
            var togglProject = ChartData!.GetTogglProjectWithTogglId(togglP);
            if (i != planProject.TogglProjectIds.Count)
            {
                trackedDurations = togglProject.GetDurationsPerDateInTimeRange(DateTime.MinValue, DateTime.MaxValue, trackedDurations, planProject.TogglProjectIds[togglP], false);
            }
            else
            {
                trackedDurations = togglProject.GetDurationsPerDateInTimeRange(DateFilter.StartDate, DateFilter.EndDate, trackedDurations, planProject.TogglProjectIds[togglP], true);
            }
        }
        trackedDurations.Keys.ToList().ForEach(date => trackedDurationsDates.Add(date));
        trackedDurations.Values.ToList().ForEach(time => trackedDurationsTimes.Add(time));

        return new List<ITrace>
        {
            new Scatter
            {
                X = plannedDurationsDates,
                Y = plannedDurationsTimes,
                Name = "Planned",
                Mode = ModeFlag.Lines| ModeFlag.Markers,
                Marker = PlannedDurationLine,
            },
            new Scatter
            {
                X = trackedDurationsDates,
                Y = trackedDurationsTimes,
                Name = "Tracked",
                Mode = ModeFlag.Lines| ModeFlag.Markers,
                Marker = TrackedDurationLine,
            },
        };
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ChartsState.Value.Loaded = false;
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