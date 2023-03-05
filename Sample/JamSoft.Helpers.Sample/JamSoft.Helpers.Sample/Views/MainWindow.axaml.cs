using System;
using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using JamSoft.Helpers.Sample.Models;
using JamSoft.Helpers.Sample.ViewModels;
using ReactiveUI;

namespace JamSoft.Helpers.Sample.Views;

public partial class MainWindow : ReactiveWindow<MainWindowSampleAppViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(disposables => 
        {
#pragma warning disable CS8634
#pragma warning disable CS8631
            this.BindCommand(ViewModel,
                    x => x.LoadPeopleCommand,
                    x => x.LoadPeopleButton)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel,
                    x => x.ValidatePeopleCommand,
                    x => x.ValidatePeopleButton)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel,
                    x => x.ValidatePropertiesCommand,
                    x => x.ValidatePropertiesButton)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel,
                    x => x.ReValidatePropertiesCommand,
                    x => x.ReValidatePropertiesButton)
                .DisposeWith(disposables);
#pragma warning restore CS8631
#pragma warning restore CS8634
            
            this.OneWayBind(ViewModel,
                    x => x.People,
                    x => x.PeopleGrid.Items)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel,
                    x => x!.SaveSettingsFileCommand,
                    x => x.SaveSettings)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel,
                    x => x!.ResetSettingsFileCommand,
                    x => x.ResetSettings)
                .DisposeWith(disposables);            
        });
    }

    private void TopLevel_OnClosed(object? sender, EventArgs e)
    {
        SampleAppSettings.Save();
    }
}