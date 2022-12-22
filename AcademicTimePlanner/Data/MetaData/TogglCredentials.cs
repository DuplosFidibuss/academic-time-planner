using System.ComponentModel.DataAnnotations;

namespace AcademicTimePlanner.Data.MetaData
{
    public class TogglCredentials
    {
        [Required]
        [MinLength(1)]
        public string TogglApiKey { get; set; }

        [Required]
        [MinLength(1)]
        public string TogglWorkspaceId { get; set; }
    }
}
