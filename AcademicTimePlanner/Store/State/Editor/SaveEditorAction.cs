namespace AcademicTimePlanner.Store.State.Editor;

public class SaveEditorAction
{
    public List<string> FieldValues { get; }
    
    public SaveEditorAction(List<string> fieldValues)
    {
        FieldValues = fieldValues;
    }
}