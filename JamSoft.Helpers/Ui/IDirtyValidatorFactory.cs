namespace JamSoft.Helpers.Ui;

/// <summary>
/// A factory class that creates dirty validators
/// </summary>
public static class DirtyValidatorFactory
{
    /// <summary>
    /// Create a new dirty validator
    /// </summary>
    /// <returns>A new <see cref="IDirtyValidator"/> instance</returns>
    public static IDirtyValidator Create()
    {
        return new IsDirtyValidator();
    }
}