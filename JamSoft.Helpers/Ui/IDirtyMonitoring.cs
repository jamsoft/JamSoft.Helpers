namespace JamSoft.Helpers.Ui;

/// <summary>
/// The IDirtyMonitoring interface. Implement this on objects you wish to validate.
/// </summary>
public interface IDirtyMonitoring
{
    /// <summary>
    /// A flag denoting if the object is dirty
    /// </summary>
    bool IsDirty { get; set; }
    
    /// <summary>
    /// The object hash value
    /// </summary>
    string? Hash { get; set; }
}