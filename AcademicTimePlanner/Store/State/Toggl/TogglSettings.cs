﻿using System.ComponentModel.DataAnnotations;

namespace AcademicTimePlanner.Store.State.Toggl
{
    public class TogglSettings
    {
        [Required]
        [MinLength(1)]
        public string TogglApiKey { get; set; }

        [Required]
        [MinLength(1)]
        public string TogglWorkspaceId { get; set; }
    }
}
