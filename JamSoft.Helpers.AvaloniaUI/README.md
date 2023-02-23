# JamSoft.Helpers.AvaloniaUI

A collection of general helpers for AvaloniaUI applications.

## Table of Contents
- [Installation](#Installation)
- [Mvvm ViewModelBase](#Mvvm-ViewModelBase)
- [IsDirty Color Converter](#IsDirty-Color-Converter)
- [Mvvm ValidatableAvaloniaViewModelBase](#Mvvm-ValidatableAvaloniaViewModelBase)

# Installation
### Nuget
```
Install-Package JamSoft.Helpers.AvaloniaUI
```
### CLI
```
dotnet add package JamSoft.Helpers.AvaloniaUI
```
# Patterns

## Mvvm ViewModelBase
A very bare bones view model with property changed updates
```csharp
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
```
## Mvvm ValidatableAvaloniaViewModelBase
```csharp
/// <summary>
/// A base view model class for use in AvaloniaUI applications with validation 
/// </summary>
public class ValidatableAvaloniaViewModelBase : AvaloniaViewModelBase, IDirtyMonitoring
{
    private bool _isDirty;
    private IEnumerable<PropertyInfo>? _changedProperties;
    private IEnumerable<FieldInfo>? _changedFields;
    private string? _hash;

    /// <summary>
    /// Default ctor
    /// </summary>
    public ValidatableAvaloniaViewModelBase() { }

    /// <summary>
    /// <inheritdoc cref="IDirtyMonitoring.IsDirty"/>
    /// </summary>
    public bool IsDirty
    {
        get => _isDirty;
        set => this.RaiseAndSetIfChanged(ref _isDirty, value);
    }

    /// <summary>
    /// <inheritdoc cref="IDirtyMonitoring.Hash"/>
    /// </summary>
    public string? Hash
    {
        get => _hash;
        set => this.RaiseAndSetIfChanged(ref _hash, value);
    }

    /// <summary>
    /// Properties containing changes
    /// </summary>
    public IEnumerable<PropertyInfo>? ChangedProperties
    {
        get => _changedProperties;
        set => this.RaiseAndSetIfChanged(ref _changedProperties, value);
    }

    /// <summary>
    /// Fields containing changes
    /// </summary>
    public IEnumerable<FieldInfo>? ChangedFields
    {
        get => _changedFields;
        set => this.RaiseAndSetIfChanged(ref _changedFields, value);
    }

    /// <summary>
    /// Validate this instance
    /// </summary>
    public virtual void Validate()
    {
        IsDirtyValidator.Validate(this);
    }
    
    /// <summary>
    /// Validates this instance with full property tracking
    /// </summary>
    public virtual void StartValidateAndTrack()
    {
        IsDirtyValidator.Validate(this, true);
    }
    
    /// <summary>
    /// Validates this instance with full property tracking
    /// </summary>
    public virtual void GetChanged()
    {
        (ChangedProperties, ChangedFields) = IsDirtyValidator.ValidatePropertiesAndFields(this);
    }

    /// <summary>
    /// Stop tracking this instance
    /// </summary>
    public virtual void StopTracking()
    {
        IsDirtyValidator.StopTrackingObject(this);
    }
}
```
### IsDirty Color Converter
```csharp
/// <summary>
/// A binding helper for IsDirty UI Feedback
/// </summary>
public class IsDirtyColorConverter : IValueConverter
{
	/// <summary>
	/// The IsDirty=False color
	/// </summary>
	public SolidColorBrush FalseColor { get; set; } = new SolidColorBrush(Color.Parse("#B8FFB8"));

	/// <summary>
	/// The IsDirty=True color
	/// </summary>
	public SolidColorBrush TrueColor { get; set; } = new SolidColorBrush(Color.Parse("#FF8D8D"));
	
	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value != null)
		{
			switch (value)
			{
				case true:
					return TrueColor;
				case false:
					return FalseColor;
			}
		}

		return FalseColor;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return null;
	}
}
```