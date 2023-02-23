using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia.Threading;
using JamSoft.Helpers.Patterns.Mvvm;
using JamSoft.Helpers.Sample.Models;
using ReactiveUI;

namespace JamSoft.Helpers.Sample.ViewModels;

public class MainWindowSampleAppViewModel : SampleAppViewModelBase, IActivatableViewModel
{
    private readonly ObservableAsPropertyHelper<SuperObservableCollection<PersonViewModel>> _people;

    public MainWindowSampleAppViewModel()
    {
        Activator = new ViewModelActivator();
        this.WhenActivated(disposables =>
        {
            /* handle activation */
            Disposable
                .Create(() => { /* handle deactivation */ })
                .DisposeWith(disposables);
        });

        LoadPeopleCommand = ReactiveCommand.CreateFromTask(LoadPeople);
        ValidatePeopleCommand = ReactiveCommand.CreateFromTask(ValidatePeople);
        ValidatePropertiesCommand = ReactiveCommand.CreateFromTask(ValidateProperties);
        ReValidatePropertiesCommand = ReactiveCommand.CreateFromTask(ReValidateProperties);
        
        _people = LoadPeopleCommand.ToProperty(this, x => x.People, scheduler: RxApp.MainThreadScheduler);
    }
    
    public ReactiveCommand<Unit, SuperObservableCollection<PersonViewModel>> LoadPeopleCommand { get; }
    public ReactiveCommand<Unit, Unit> ValidatePeopleCommand { get; }
    public ReactiveCommand<Unit, Unit> ValidatePropertiesCommand { get; }
    
    public ReactiveCommand<Unit, Unit> ReValidatePropertiesCommand { get; }

    private Task ValidateProperties()
    {
        return Task.Run(() =>
        {
            foreach (var personViewModel in People)
            {
                Dispatcher.UIThread.Post(() => personViewModel.StartValidateAndTrack());
            }
        });
    }
    
    private Task ReValidateProperties()
    {
        return Task.Run(() =>
        {
            foreach (var personViewModel in People)
            {
                Dispatcher.UIThread.Post(() => personViewModel.GetChanged());
            }
        });
    }

    private Task ValidatePeople()
    {
        return Task.Run(() =>
        {
            foreach (var personViewModel in People)
            {
                Dispatcher.UIThread.Post(() => personViewModel.Validate());
            }
        });
    }

    private async Task<SuperObservableCollection<PersonViewModel>> LoadPeople()
    {
        return await Task.Run(DataSource.GetPeople);
    }

    public SuperObservableCollection<PersonViewModel> People => _people.Value;
    public ViewModelActivator Activator { get; }
}