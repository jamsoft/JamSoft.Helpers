using System.Reflection;
using JamSoft.Helpers.Ui;
using ReactiveUI;

namespace JamSoft.Helpers.AvaloniaUI.Patterns.Mvvm;

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