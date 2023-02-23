using ReactiveUI;

namespace JamSoft.Helpers.AvaloniaUI.Patterns.Mvvm;

/// <summary>
/// A base view model class for use in AvaloniaUI applications
/// </summary>
public abstract class AvaloniaViewModelBase : ReactiveObject
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