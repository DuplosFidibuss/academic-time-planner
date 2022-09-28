namespace AcademicTimePlanner.Services.BootstrapModalService;

public interface IBootstrapModalService
{
    Task ShowModalAsync();
    Task HideModalAsync();
}