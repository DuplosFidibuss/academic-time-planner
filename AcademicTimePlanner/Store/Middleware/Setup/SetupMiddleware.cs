using AcademicTimePlanner.Store.State.TogglSettings;
using Fluxor;

namespace AcademicTimePlanner.Store.Middleware.Setup;

public class SetupMiddleware : Fluxor.Middleware
{
    private IConfiguration _configuration;

    public override Task InitializeAsync(IDispatcher dispatcher, IStore store)
    {
        try
        {
            var togglApiKey = _configuration.GetValue<string>("TogglApiKey");
            var togglWorkspaceId = _configuration.GetValue<string>("TogglWorkspaceId");
            dispatcher.Dispatch(new SetTogglSettingsAction(togglApiKey, togglWorkspaceId));
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("No appsettings with TogglApiKey and TogglWorkspaceId found.");
        }
        
        
        return Task.CompletedTask;
    }
}