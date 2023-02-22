using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using JamSoft.Helpers.Sample.ViewModels;
using JamSoft.Helpers.Sample.Views;

namespace JamSoft.Helpers.Sample;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowSampleAppViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}