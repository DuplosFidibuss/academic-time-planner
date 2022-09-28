using AcademicTimePlanner.Services.BootstrapModalService;
using AcademicTimePlanner.Store.State.TogglSettings;
using Fluxor;

namespace AcademicTimePlanner.Store.State.Editor;

public class Effects
{
    private IBootstrapModalService _bootstrapModalService;
    private IState<EditorState> _editorState;
    private const string EditorTypeTogglCredentials = "TogglCredentials";

    public Effects(IBootstrapModalService bootstrapModalService, IState<EditorState> editorState)
    {
        _bootstrapModalService = bootstrapModalService;
        _editorState = editorState;
    }

    [EffectMethod]
    public async Task HandleAsync(ShowEditorAction action, IDispatcher dispatcher)
    {
        await _bootstrapModalService.ShowModalAsync();
    }

    [EffectMethod]
    public async Task HandleAsync(SaveEditorAction action, IDispatcher dispatcher)
    {
        switch (_editorState.Value.EditorType)
        {
            case EditorTypeTogglCredentials:
                var values = action.FieldValues;
                dispatcher.Dispatch(new SetTogglSettingsAction(values[0], values[1]));
                break;
            default:
                throw new Exception("Unsupported editor type");
        }
        await _bootstrapModalService.HideModalAsync();
    }

    [EffectMethod]
    public async Task HandleAsync(EditorAskForTogglCredentialsAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(
            new ShowEditorAction(EditorTypeTogglCredentials,
            new List<Field>
            {
                new("Toggl API Key", "String", ""),
                new("Toggl Workspace Id", "String", "")
            }));
    }
}