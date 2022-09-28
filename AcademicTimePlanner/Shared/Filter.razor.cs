using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AcademicTimePlanner.Shared;

public partial class Filter
{ 
    [Inject]
    public IJSRuntime JSRuntime { get; set; }
    private IJSObjectReference _genericFilterJsModule;

    protected override async Task OnInitializedAsync()
    {
        _genericFilterJsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/filter.js");
    }

    private async Task OpenStartDatePicker()
    {
        await _genericFilterJsModule.InvokeVoidAsync("OpenStartDatePicker");
    }
    
    private async Task OpenEndDatePicker()
    {
        await _genericFilterJsModule.InvokeVoidAsync("OpenEndDatePicker");
    }
}