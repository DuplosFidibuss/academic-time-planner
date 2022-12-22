﻿using AcademicTimePlanner.Data.DisplayData;

namespace AcademicTimePlanner.Store.State.ProjectLinker
{
    public class SetProjectsDataAction
    {
        public ProjectsData ProjectsData { get; set; }

        public SetProjectsDataAction(ProjectsData projectsData)
        {
            ProjectsData = projectsData;
        }
    }
}
