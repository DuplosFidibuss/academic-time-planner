using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AcademicTimePlanner.Pages
{
    public partial class ProjectLinker
    {
        [Inject]
        private IDispatcher Dispatcher { get; set; }

        private const string Title = "Link projects";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dispatcher.Dispatch(new SetTitleAction(Title));
        }
    }
}