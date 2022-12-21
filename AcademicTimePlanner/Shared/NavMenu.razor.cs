namespace AcademicTimePlanner.Shared
{
    public partial class NavMenu
    {
        private bool _collapseNavMenu = true;

        private string? _navMenuCssClass => _collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }
    }
}