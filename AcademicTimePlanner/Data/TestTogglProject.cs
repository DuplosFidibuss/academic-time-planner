using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public static class TestTogglProject
    {
        private static readonly DateTime date_1 = new DateTime(2022, 10, 16);
        private static readonly DateTime date_2 = new DateTime(2022, 10, 23);
        private static readonly DateTime date_3 = new DateTime(2022, 10, 31);
        private static readonly DateTime date_4 = new DateTime(2022, 11, 7);

        private static readonly TogglEntrySum togglEntrySum_1 = new TogglEntrySum(date_1, 3, 1, 1);
        private static readonly TogglEntrySum togglEntrySum_2 = new TogglEntrySum(date_2, 1, 2, 1);
        private static readonly TogglEntrySum togglEntrySum_3 = new TogglEntrySum(date_3, 1.5, 3, 1);
        private static readonly TogglEntrySum togglEntrySum_4 = new TogglEntrySum(date_4, 2, 4, 1);


        public static List<TogglProject> GetTestTogglProject()
        {
            List<TogglProject> togglProjects = new List<TogglProject>();

            var togglProject_1 = new TogglProject(1, "TestTogglProject_1");
            var togglProject_2 = new TogglProject(2, "TestTogglProject_2");

            togglProject_1.AddEntry(togglEntrySum_1);
            togglProject_1.AddEntry(togglEntrySum_2);
            togglProject_1.AddEntry(togglEntrySum_3);
            togglProject_1.AddEntry(togglEntrySum_4);

            togglProject_2.AddEntry(togglEntrySum_1);
            togglProject_2.AddEntry(togglEntrySum_2);
            togglProject_2.AddEntry(togglEntrySum_3);
            togglProject_2.AddEntry(togglEntrySum_4);

            togglProjects.Add(togglProject_1);
            togglProjects.Add(togglProject_2);
            return togglProjects;
        }
    }
}
