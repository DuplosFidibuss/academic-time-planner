using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public static class TestTogglProject
    {
        private static readonly DateTime date_1 = new DateTime(2022, 10, 1);
        private static readonly DateTime date_2 = new DateTime(2022, 10, 10);
        private static readonly DateTime date_3 = new DateTime(2022, 10, 16);
        private static readonly DateTime date_4 = new DateTime(2022, 10, 22);

        private static readonly TogglEntrySum togglEntrySum_1 = new TogglEntrySum(date_1, 3, 1, 1);
        private static readonly TogglEntrySum togglEntrySum_2 = new TogglEntrySum(date_2, 1, 2, 1);
        private static readonly TogglEntrySum togglEntrySum_3 = new TogglEntrySum(date_3, 1.5, 3, 1);
        private static readonly TogglEntrySum togglEntrySum_4 = new TogglEntrySum(date_4, 2, 4, 1);

        private static void addEntries(TogglTask task)
        {
            task.AddEntry(togglEntrySum_1);
            task.AddEntry(togglEntrySum_2);
            task.AddEntry(togglEntrySum_3);
            task.AddEntry(togglEntrySum_4);
        }

        public static TogglProject GetTestTogglProject() {
            var togglTask = new TogglTask(1, "TestTogglTask_1");
            addEntries(togglTask);
            var togglProject = new TogglProject(1, "TestTogglProject_1");
            togglProject.AddTogglTask(togglTask);
            return togglProject; 
        }
    }
}
