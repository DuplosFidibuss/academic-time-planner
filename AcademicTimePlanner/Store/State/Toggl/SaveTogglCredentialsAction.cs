using AcademicTimePlanner.Data.MetaData;

namespace AcademicTimePlanner.Store.State.Toggl
{
    public class SaveTogglCredentialsAction
    {
		public TogglCredentials TogglSettings { get; set; }

		public SaveTogglCredentialsAction(TogglCredentials togglSettings)
		{
			TogglSettings = togglSettings;
		}
    }
}
