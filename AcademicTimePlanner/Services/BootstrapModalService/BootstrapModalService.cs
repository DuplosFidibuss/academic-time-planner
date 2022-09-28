using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AcademicTimePlanner.Services.BootstrapModalService;

public class BootstrapModalService : IBootstrapModalService
{
    private IJSRuntime JsRuntime { get; set; }
    private IJSObjectReference bootstrapModalHelper;

    private const string PathToBootstrapModalHelpersJsModule = "./scripts/bootstrap-modal-helpers.js";
    private const string ShowModalByIdFunctionName = "showModalById";
    private const string HideModalByIdFunctionName = "hideModalById";
    private const string ModalId = "exampleModal";

    public BootstrapModalService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }
    
    public async Task ShowModalAsync()
    {
        bootstrapModalHelper = await JsRuntime.InvokeAsync<IJSObjectReference>("import", PathToBootstrapModalHelpersJsModule);
        await bootstrapModalHelper.InvokeVoidAsync(ShowModalByIdFunctionName, ModalId);
    }

    public async Task HideModalAsync()
    {
        bootstrapModalHelper = await JsRuntime.InvokeAsync<IJSObjectReference>("import", PathToBootstrapModalHelpersJsModule);
        await bootstrapModalHelper.InvokeVoidAsync(HideModalByIdFunctionName, ModalId);
    }
}