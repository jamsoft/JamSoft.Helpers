using System;
using System.Linq;

namespace JamSoft.Helpers.Ui;

/// <summary>
/// 
/// </summary>
internal static class IsDirtyExtensions 
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static Guid GetId(this IDirtyMonitoring? instance)
    {
        if (instance == null || 
            string.IsNullOrWhiteSpace(instance.Hash) || 
            !instance.Hash!.Contains('|')) 
            return Guid.Empty;
		
        return Guid.Parse(instance.Hash!.Split('|')[0]);
    }
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static string GetIsDirtyHash(this IDirtyMonitoring? instance)
    {
        if (instance == null ||
            string.IsNullOrWhiteSpace(instance.Hash) || 
            !instance.Hash!.Contains('|'))
            return string.Empty;
		
        return instance.Hash!.Split('|')[1];
    }
}