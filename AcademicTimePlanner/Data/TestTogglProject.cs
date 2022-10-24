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

        private static readonly TogglTask togglTask = new TogglTask(1, "TestTogglTask_1");
        

        private static readonly TogglProject togglProject = new TogglProject(1, "TestTogglProject_1");

        private static void addEntries()
        {
            togglTask.AddEntry(togglEntrySum_1);
            togglTask.AddEntry(togglEntrySum_2);
            togglTask.AddEntry(togglEntrySum_3);
            togglTask.AddEntry(togglEntrySum_4);
        }

        private static void addTasks()
        {
            togglProject.AddTogglTask(togglTask);
        }

        public static TogglProject GetTestTogglProject() { 
            addEntries();
            addTasks();
            return togglProject; 
        }
    }
}
