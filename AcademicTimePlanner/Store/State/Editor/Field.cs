namespace AcademicTimePlanner.Store.State.Editor;

public class Field
{
    public string Label { get; }
    public string Type { get; }
    public string InitialValue { get; }

    public Field(string label, string type, string initialValue)
    {
        Label = label;
        Type = type;
        InitialValue = initialValue;
    }
}