using AcademicTimePlanner.Data.ApplicationData.Plan;
using AcademicTimePlanner.Store.State.ProjectFiles;
using AcademicTimePlanner.Store.State.Wrapper;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace AcademicTimePlanner.Pages
{
    public partial class PlanProjects
    {
        private const string Title = "Plan projects";

        [Inject]
        private IState<ProjectFilesState> _projectState { get; set; }

        [Inject]
        private IDispatcher _dispatcher { get; set; }

        [Inject]
        public IJSRuntime _jsRuntime { get; set; }

        private List<PlanProject> _projects => _projectState.Value.PlanProjects;

        private PlanProject? _planProject => _projectState.Value.PlanProject;

        private PlanTask? _planTask => _projectState.Value.PlanTask;

        private PlanEntry? _planEntry => _projectState.Value.PlanEntry;

        private PlanEntryRepetition? _planEntryRepetition => _projectState.Value.PlanEntryRepetition;

        private string _oldPlanProjectName;
        private List<PlanTask> _deletedPlanTaks = new List<PlanTask>();
        private List<PlanEntry> _deletedPlanEntries = new List<PlanEntry>();
        private List<PlanEntryRepetition> _deletedPlanEntryRepetitions = new List<PlanEntryRepetition>();
        private List<PlanTask> _addedPlanTasks = new List<PlanTask>();
        private List<PlanEntry> _addedPlanEntries = new List<PlanEntry>();
        private List<PlanEntryRepetition> _addedPlanEntryRepetitions = new List<PlanEntryRepetition>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _dispatcher.Dispatch(new SetTitleAction(Title));
            _dispatcher.Dispatch(new FetchPlanProjectsAction());
        }

        private async Task LoadPlanProjects(InputFileChangeEventArgs e)
        {
            var json = new List<string>();
            foreach (var file in e.GetMultipleFiles(int.MaxValue))
            {
                json.Add(await new StreamReader(file.OpenReadStream()).ReadToEndAsync());
            }
            _dispatcher.Dispatch(new LoadPlanProjectsAction(json));
        }

        private void CreatePlanProject()
        {
            _dispatcher.Dispatch(new SwitchCreationStepAction(ProjectFilesState.CreationStep.NamingProject, new PlanProject(Guid.NewGuid())));
        }

        private void CreatePlanTask()
        {
            if (_planTask!.IsValidPlanTask())
            {
                _addedPlanTasks.Add(_planTask);
                _dispatcher.Dispatch(new CreatePlanTaskAction(_planTask));
            }
        }

        private void CreatePlanEntry()
        {
            if (_planEntry!.IsValidPlanEntry())
            {
                _addedPlanEntries.Add(_planEntry);
                _planProject!.AddPlanEntry(_planEntry);
                _dispatcher.Dispatch(new AddSingleEntryAction());
            }
        }

        private void CreatePlanEntryRepetition()
        {
            if (_planEntryRepetition!.IsValidPlanEntryRepetition())
            {
                _addedPlanEntryRepetitions.Add(_planEntryRepetition);
                _planEntryRepetition.Modify();
                _planProject!.AddPlanEntryRepetition(_planEntryRepetition);
                _dispatcher.Dispatch(new AddRepetitionEntryAction());
            }
        }

        private async Task DownloadPlanProject(PlanProject planProject)
        {
            var fileStream = new MemoryStream();
            var writer = new StreamWriter(fileStream);
            writer.Write(JsonConvert.SerializeObject(planProject, Formatting.Indented));
            writer.Flush();
            fileStream.Position = 0;
            using var streamRef = new DotNetStreamReference(stream: fileStream);
            await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", planProject!.Name + ".json", streamRef);
        }

        private void EditPlanProject(EventArgs e, PlanProject planProject)
        {
            _oldPlanProjectName = planProject.Name;
            _dispatcher.Dispatch(new SwitchCreationStepAction(ProjectFilesState.CreationStep.NamingProject, planProject));
        }

        private void AddSingleEntry()
        {
            _dispatcher.Dispatch(new AddSingleEntryAction());
        }

        private void AddRepetitionEntry()
        {
            _dispatcher.Dispatch(new AddRepetitionEntryAction());
        }

        private void DeletePlanProject(EventArgs e, Guid planProjectId)
        {
            _dispatcher.Dispatch(new DeletePlanProjectAction(planProjectId));
        }

        private void DeletePlanTask(EventArgs e, PlanTask planTask)
        {
            _planProject!.RemovePlanTask(planTask);
            _deletedPlanTaks.Add(planTask);
        }

        private void DeletePlanEntry(EventArgs e, PlanEntry planEntry)
        {
            _planProject!.RemovePlanEntry(planEntry);
            _deletedPlanEntries.Add(planEntry);
        }

        private void DeletePlanEntryRepetition(EventArgs e, PlanEntryRepetition entryRepetition)
        {
            _planProject!.RemovePlanEntryRepetition(entryRepetition);
            _deletedPlanEntryRepetitions.Add(entryRepetition);
        }

        private void NextOrBack(ProjectFilesState.CreationStep step)
        {
            _dispatcher.Dispatch(new SwitchCreationStepAction(step, _planProject!));
        }

        private void Finish()
        {
            if (!string.IsNullOrWhiteSpace(_planProject!.Name))
                _dispatcher.Dispatch(new FinishPlanProjectCreationAction(_planProject!));
        }

        private void Cancel()
        {
            RevertChangesOnProject();
            _dispatcher.Dispatch(new SwitchCreationStepAction(ProjectFilesState.CreationStep.NotCreating, null));
        }

        private void RevertChangesOnProject()
        {
            _planProject!.Name = _oldPlanProjectName;
            _deletedPlanTaks.ForEach(task => _planProject!.AddPlanTask(task));
            _deletedPlanEntries.ForEach(entry => _planProject!.AddPlanEntry(entry));
            _deletedPlanEntryRepetitions.ForEach(entry => _planProject!.AddPlanEntryRepetition(entry));
            _addedPlanTasks.ForEach(task => _planProject!.RemovePlanTask(task));
            _addedPlanEntries.ForEach(entry => _planProject!.RemovePlanEntry(entry));
            _addedPlanEntryRepetitions.ForEach(entry => _planProject!.RemovePlanEntryRepetition(entry));
        }
    }
}