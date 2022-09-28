using Fluxor;

namespace AcademicTimePlanner.Store.Middleware.Persistence;

public class PersistenceMiddleware : Fluxor.Middleware
{
    private IDispatcher _dispatcher;
    private IStore _store;

    public override Task InitializeAsync(IDispatcher dispatcher, IStore store)
    {
        _dispatcher = dispatcher;
        _store = store;
        return Task.CompletedTask;
    }

    public override void AfterDispatch(object action)
    {
        if (action is not PersistStateAction)
        {
            _dispatcher.Dispatch(new PersistStateAction());
        }
    }
}