using AcademicTimePlanner.ApplicationData.Toggl;

namespace AcademicTimePlanner.Tests
{
    public static class TestTogglProject
    {
        public static List<TogglProject> GetTestTogglProject()
        {
            var togglProjects = new List<TogglProject>();

            var togglProject_1 = new TogglProject(1, "TestTogglProject_1");
            var togglProject_2 = new TogglProject(2, "TestTogglProject_2");

            togglProject_1.AddEntry(new TogglEntrySum(new DateTime(2022, 10, 16), 3, 11, 1));
            togglProject_1.AddEntry(new TogglEntrySum(new DateTime(2022, 10, 23), 1, 12, 1));
            togglProject_1.AddEntry(new TogglEntrySum(new DateTime(2022, 10, 31), 1.5, 13, 1));
            togglProject_1.AddEntry(new TogglEntrySum(new DateTime(2022, 11, 7), 2, 14, 1));

            togglProject_1.AddTogglTask(1, "TestTogglTask_1");

            togglProject_2.AddEntry(new TogglEntrySum(new DateTime(2022, 10, 7), 1.5, 21, 1));
            togglProject_2.AddEntry(new TogglEntrySum(new DateTime(2022, 10, 18), 2, 22, 1));
            togglProject_2.AddEntry(new TogglEntrySum(new DateTime(2022, 10, 26), 2.3, 23, 1));
            togglProject_2.AddEntry(new TogglEntrySum(new DateTime(2022, 11, 2), 1.75, 24, 1));

            togglProject_2.AddTogglTask(2, "TestTogglTask_2");
            togglProject_2.AddTogglTask(3, "TestTogglTask_3");

            togglProjects.Add(togglProject_1);
            togglProjects.Add(togglProject_2);

            return togglProjects;
        }
    }
}
