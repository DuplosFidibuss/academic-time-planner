using AcademicTimePlanner.Store.State.Editor;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Infrastructure;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace AcademicTimePlanner.Shared;

public partial class Editor
{
    [Inject] 
    private IDispatcher Dispatcher { get; set; }
    
    [Inject] 
    private IState<EditorState> EditorState { get; set; }

    public List<string>? FieldValues;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        EditorState.StateChanged += EditorState_StateChanged;
    }

    private void EditorState_StateChanged(object? sender, EventArgs eventArgs)
    {
        FieldValues = EditorState.Value.Fields?.Select(field => field.InitialValue).ToList();
    }

    private void SaveChanges()
    {
        Dispatcher.Dispatch(new SaveEditorAction(FieldValues));
    }
}