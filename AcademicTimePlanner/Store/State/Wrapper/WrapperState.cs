using Fluxor;

namespace AcademicTimePlanner.Store.State.Wrapper;

[FeatureState]
public class WrapperState
{
    public string Title { get; }

    private WrapperState()
    {
    }

    public WrapperState(string title)
    {
        Title = title;
    }
}