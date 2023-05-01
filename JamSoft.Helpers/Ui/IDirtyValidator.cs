using System;
using System.Collections.Generic;
using System.Reflection;

namespace JamSoft.Helpers.Ui;

/// <summary>
/// The IDirtyValidator interface
/// </summary>
public interface IDirtyValidator
{
    /// <summary>
    /// The Object value hash store
    /// </summary>
    Dictionary<Guid, Dictionary<string, string>?> ObjectValueHashStore { get; set; }
    
    /// <summary>
    /// Validates properties and fields decorated with <see cref="IsDirtyMonitoringAttribute"/> on whole instances for dirtiness. If any monitored value has changed, the IsDirty property will be set to True 
    /// </summary>
    /// <param name="instance">The instance to be validated</param>
    /// <param name="trackProperties"></param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>The sample annotated object instance</returns>
    /// <remarks>Validate the clean object in order to calculate the clean hash value for the object instance. Subsequent calls will detect if the contents has changes between validations</remarks>
    T? Validate<T>(T? instance, bool trackProperties = false) where T : IDirtyMonitoring, new();

    /// <summary>
    /// Checks individual values within properties and fields and returns a list of each with changes
    /// </summary>
    /// <remarks>For a call to this method to work, the object must have been previously validated with by calling the Validate method with trackProperties set to true</remarks>
    /// <param name="instance">The object instance to validate</param>
    /// <typeparam name="T">The object type</typeparam>
    /// <returns>Two arrays, one of <see cref="PropertyInfo"/> objects and one of <see cref="FieldInfo"/> objects</returns>
    (PropertyInfo[] properties, FieldInfo[] fields) ValidatePropertiesAndFields<T>(T? instance) where T : IDirtyMonitoring, new();

    /// <summary>
    /// Resets all tracking data
    /// </summary>
    void Reset(bool clearTypeInfo = false);

    /// <summary>
    /// Stops tracking an object by clearing down any stored information and resetting the properties
    /// </summary>
    /// <param name="instance">The object to stop tracking</param>
    /// <typeparam name="T">The object type</typeparam>
    void StopTrackingObject<T>(T? instance) where T : IDirtyMonitoring, new();
}