using Fluxor;

namespace AcademicTimePlanner.Store.State.Editor;

public class Reducers
{
    [ReducerMethod]
    public EditorState Reduce(EditorState state, ShowEditorAction action) => new(action.EditorType, action.Fields);
}