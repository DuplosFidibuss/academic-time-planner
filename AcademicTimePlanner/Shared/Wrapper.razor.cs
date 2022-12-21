using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace AcademicTimePlanner.Shared
{
    public partial class Wrapper
    {
        [Inject]
        private IState<WrapperState> _wrapperState { get; set; }
    }
}