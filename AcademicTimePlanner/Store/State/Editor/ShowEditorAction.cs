namespace AcademicTimePlanner.Store.State.Editor;

public class ShowEditorAction
{
    public string EditorType { get; }
    public List<Field> Fields { get; }
    
    public ShowEditorAction(string editorType, List<Field> fields)
    {
        EditorType = editorType;
        Fields = fields;
    }
}