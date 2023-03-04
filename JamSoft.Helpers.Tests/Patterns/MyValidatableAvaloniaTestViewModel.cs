using JamSoft.Helpers.AvaloniaUI.Patterns.Mvvm;
using JamSoft.Helpers.Ui;

namespace JamSoft.Helpers.Tests.Patterns;

internal sealed class MyValidatableAvaloniaTestViewModel : ValidatableAvaloniaViewModelBase
{
    [IsDirtyMonitoring]
    public string MonitoredValue { get; set; }
        
    public string UnMonitoredValue { get; set; }
}