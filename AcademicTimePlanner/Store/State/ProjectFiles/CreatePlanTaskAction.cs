﻿using AcademicTimePlanner.DataMapping.Plan;

namespace AcademicTimePlanner.Store.State.ProjectFiles
{
    public class CreatePlanTaskAction
    {
        public PlanTask PlanTask { get; }

        public CreatePlanTaskAction(PlanTask planTask)
        {
            PlanTask = planTask;
        }
    }
}
