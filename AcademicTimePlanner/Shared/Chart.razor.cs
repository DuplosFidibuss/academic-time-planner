using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AcademicTimePlanner.Shared;

public partial class Chart
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public string? ChartType { get; set; }
    [Parameter]
    public string? CanvasId { get; set; }
    [Parameter]
    public List<(double Number, string Label)>? ChartData { get; set; }
    [Parameter]
    public string? ChartTitle { get; set; }

    private IJSObjectReference _genericChartJsModule;

    protected override async Task OnInitializedAsync()
    {
        _genericChartJsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/genericChart.js");
        await GenerateGenericChart();
    }

    public async Task GenerateGenericChart()
    {
        var functionName = "generatePieChart";
        
        await _genericChartJsModule.InvokeAsync<string>(functionName, CanvasId, ChartType, ChartData.Select(data => data.Number), ChartData.Select(label => label.Label), ChartTitle);
    }
}