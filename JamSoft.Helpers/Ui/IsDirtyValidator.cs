using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using JamSoft.Helpers.Strings;

namespace JamSoft.Helpers.Ui;

/// <summary>
/// A decorator class for dirty monitoring
/// </summary>
public static class IsDirtyValidator
{
	/// <summary>
	/// Validates whole instances for dirtiness. If any monitored value has changed IsDirty will be set to True
	/// </summary>
	/// <param name="obj">The instance to be validated</param>
	/// <param name="reset">pass in true to reset validation on the instance</param>
	/// <typeparam name="T">The object type</typeparam>
	/// <returns>The sample annotated object instance</returns>
	/// <remarks>Validate the clean object in order to calculate the clean hash value for the object instance. Subsequent calls will detect if the contents has changes between validations</remarks>
	public static T Validate<T>(T obj, bool reset = false) where T : IDirtyMonitoring
	{
		if (reset) obj.Hash = string.Empty;
		
		if (string.IsNullOrWhiteSpace(obj.Hash))
		{
			obj.Hash = GetObjectHash(obj);
			obj.IsDirty = false;
		}
		else
		{
			var newHash = GetObjectHash(obj);
			obj.IsDirty = !obj.Hash.IsExactlySameAs(newHash);
		}
		
		return obj;
	}
	
	/// <summary>
	/// Gets the object hash from the objects property values.
	/// </summary>
	/// <returns>An MD5 hash representing the object</returns>
	private static string GetObjectHash<T>(T obj) where T : IDirtyMonitoring 
	{
		string md5;
        try
        {
            var propertyInfos = obj.GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(IsDirtyMonitoringAttribute)) != null).ToList();
            var fieldInfos = obj.GetType().GetFields().Where(f => f.GetCustomAttribute(typeof(IsDirtyMonitoringAttribute)) != null).ToList();
            var sb = new StringBuilder();
            
            foreach (var propertyInfo in propertyInfos)
            {
	            var value = propertyInfo.GetValue(obj);
	            if (value == null)
	            {
		            sb.Append("null");
	            }
	            else
	            {
		            sb.Append(value);
	            }
            }
            
            foreach (var fieldInfo in fieldInfos)
            {
	            var value = fieldInfo.GetValue(obj);
	            if (value == null)
	            {
		            sb.Append("null");
	            }
	            else
	            {
		            sb.Append(value);
	            }
            }
            
            md5 = GetMd5Sum(Encoding.Default.GetBytes(sb.ToString()));
        }
        catch (Exception ex)
		{
			throw new Exception("Cannot calculate hash.", ex);
		}

		return md5;
	}

	/// <summary>
	/// Gets the MD5 sum from the buffer byte data.
	/// </summary>
	/// <param name="buffer">The buffer.</param>
	/// <returns>a string MD5 value</returns>
	private static string GetMd5Sum(byte[] buffer)
	{
		var md5 = MD5.Create();
		var hash = md5.ComputeHash(buffer);
		var sb = new StringBuilder();

		foreach (byte b in hash)
		{
			sb.Append(b.ToString("X2"));
		}

		return sb.ToString();
	}
}