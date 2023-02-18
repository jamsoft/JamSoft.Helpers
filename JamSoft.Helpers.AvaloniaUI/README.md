# JamSoft.Helpers.AvaloniaUI

A collection of general helpers for AvaloniaUI applications.

# Install
### Nuget
```
Install-Package JamSoft.Helpers.AvaloniaUI
```
### CLI
```
dotnet add package JamSoft.Helpers.AvaloniaUI
```
# Patterns

## Mvvm - ViewModelBase
A very bare bones view model with property changed updates
```csharp
public abstract class ViewModelBase : ReactiveObject
{
    private bool _isEditable;
    private bool _isBusy;
    
    /// <summary>
    /// Gets or sets a value indicating whether this instance is editable.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is editable; otherwise, <c>false</c>.
    /// </value>
    public bool IsEditable
    {
        get => _isEditable;
        set => this.RaiseAndSetIfChanged(ref _isEditable, value);
    }

    /// <summary>
    /// Gets or sets the busy state for the view model instance
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }
}
```