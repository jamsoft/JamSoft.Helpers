using System;

namespace JamSoft.Helpers.Ui;

/// <summary>
/// An attribute to apply to properties in order to monitor them for changes
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IsDirtyMonitoringAttribute : Attribute { }