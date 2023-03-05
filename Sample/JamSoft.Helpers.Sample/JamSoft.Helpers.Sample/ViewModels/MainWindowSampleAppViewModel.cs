using System.IO;
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
    private string? _aStringValue;
    private string? _settingsFileContents;

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
        
        SaveSettingsFileCommand = ReactiveCommand.CreateFromTask(SaveSettingsFile);
        ResetSettingsFileCommand = ReactiveCommand.CreateFromTask(ResetSettingsFile);
        
        _people = LoadPeopleCommand.ToProperty(this, x => x.People, scheduler: RxApp.MainThreadScheduler);

        AStringValue = SampleAppSettings.Instance.AStringValue;
    }

    public ReactiveCommand<Unit, Unit> ResetSettingsFileCommand { get; set; }

    public ReactiveCommand<Unit,Unit> SaveSettingsFileCommand { get; }

    public ReactiveCommand<Unit, SuperObservableCollection<PersonViewModel>> LoadPeopleCommand { get; }
    public ReactiveCommand<Unit, Unit> ValidatePeopleCommand { get; }
    public ReactiveCommand<Unit, Unit> ValidatePropertiesCommand { get; }
    
    public ReactiveCommand<Unit, Unit> ReValidatePropertiesCommand { get; }

    public string? AStringValue
    {
        get => _aStringValue;
        set
        {
            this.RaiseAndSetIfChanged(ref _aStringValue, value);
            SampleAppSettings.Instance.AStringValue = value!;
        }
    }

    public string? SettingsFileContents
    {
        get => _settingsFileContents;
        set => this.RaiseAndSetIfChanged(ref _settingsFileContents, value);
    }

    private Task SaveSettingsFile()
    {
        return Task.Run(async () =>
        {
            SampleAppSettings.Save();
            SettingsFileContents = await ReadSettingsFileContents();
        });
    }

    private async Task<string> ReadSettingsFileContents()
    {
        return await File.ReadAllTextAsync(Path.Combine(EnvEx.WhereAmI(), "sampleappsettings.json"));
    }

    private Task ResetSettingsFile()
    {
        return Task.Run(async () =>
        {
            SampleAppSettings.ResetToDefaults();
            AStringValue = SampleAppSettings.Instance.AStringValue;
            SettingsFileContents = await ReadSettingsFileContents();
        });
    }

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