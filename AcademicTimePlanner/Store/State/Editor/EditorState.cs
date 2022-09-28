using Fluxor;

namespace AcademicTimePlanner.Store.State.Editor;

[FeatureState]
public class EditorState
{
    public string EditorType { get; }
    public List<Field>? Fields { get; }
    
    private EditorState() { }

    public EditorState(string editorType, List<Field> fields)
    {
        EditorType = editorType;
        Fields = fields;
    }
}