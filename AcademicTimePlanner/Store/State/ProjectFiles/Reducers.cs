﻿using Fluxor;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class Reducers
    {
        [ReducerMethod]
        public static ProjectFilesState Reduce(ProjectFilesState state , SetPlanProjectsAction action)
        {
            return new ProjectFilesState(true, action.NumberOfPlanProjects);
        }
    }
}
