namespace AcademicTimePlanner.Store.State.Wrapper
{
    public class SetTitleAction
    {
        public string Title { get; }

        public SetTitleAction(string title)
        {
            Title = title;
        }
    }
}