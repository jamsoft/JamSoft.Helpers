using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using JamSoft.Helpers.Strings;

namespace JamSoft.Helpers.Ui;

/// <summary>
/// A utility class for monitoring objects for changes
/// </summary>
public static class IsDirtyValidator
{
	private static readonly Dictionary<int, Dictionary<string, string>> ObjectValueHashStore = new();

	/// <summary>
	/// Validates properties and fields decorated with <see cref="IsDirtyMonitoringAttribute"/> on whole instances for dirtiness. If any monitored value has changed, the IsDirty property will be set to True 
	/// </summary>
	/// <param name="obj">The instance to be validated</param>
	/// <param name="trackProperties"></param>
	/// <typeparam name="T">The object type</typeparam>
	/// <returns>The sample annotated object instance</returns>
	/// <remarks>Validate the clean object in order to calculate the clean hash value for the object instance. Subsequent calls will detect if the contents has changes between validations</remarks>
	public static T Validate<T>(T obj, bool trackProperties = false) where T : IDirtyMonitoring, new()
	{
		if (obj != null)
		{
			if (trackProperties)
			{
				var objId = obj.GetHashCode();
				obj.Hash = string.Empty;
				if (ObjectValueHashStore.ContainsKey(objId))
				{
					ObjectValueHashStore.Remove(objId);
				}
			}
		
			if (string.IsNullOrWhiteSpace(obj.Hash))
			{
				obj.Hash = GetObjectHash(obj, trackProperties);
				obj.IsDirty = false;
			}
			else
			{
				var newHash = GetObjectHash(obj, trackProperties);
				obj.IsDirty = !obj.Hash.IsExactlySameAs(newHash);
			}
		}
		
		return obj;
	}
	
	/// <summary>
	/// Checks individual values within properties and fields and returns a list of each with changes
	/// </summary>
	/// <remarks>For a call to this method to work, the object must have been previously validated with by calling the Validate method with trackProperties set to true</remarks>
	/// <param name="obj">The object instance to validate</param>
	/// <typeparam name="T">The object type</typeparam>
	/// <returns>Two arrays, one of <see cref="PropertyInfo"/> objects and one of <see cref="FieldInfo"/> objects</returns>
	public static (PropertyInfo[] properties, FieldInfo[] fields) ValidatePropertiesAndFields<T>(T obj) where T : IDirtyMonitoring, new()
	{
		List<PropertyInfo> changedProps = new List<PropertyInfo>();
		List<FieldInfo> changedFields = new List<FieldInfo>();
		
		if (obj != null && !obj.Hash.IsExactlySameAs(GetObjectHash(obj, false)))
		{
			var propertyInfos = obj.GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(IsDirtyMonitoringAttribute)) != null).ToList();
			var fieldInfos = obj.GetType().GetFields().Where(f => f.GetCustomAttribute(typeof(IsDirtyMonitoringAttribute)) != null).ToList();
			var objId = obj.GetHashCode();

			ObjectValueHashStore.TryGetValue(objId, out var objHashes);
		
			foreach (var propertyInfo in propertyInfos)
			{
				string propValue;
				object v = propertyInfo.GetValue(obj);
				if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.Name != "String")
				{
					propValue = System.Text.Json.JsonSerializer.Serialize(v);
				}
				else
				{
					propValue = v?.ToString() ?? string.Empty;
				}
				
				if (objHashes != null)
				{
					var newPropHash = GetHash(Encoding.Default.GetBytes(propValue));
					string oldPropHash;
				
					objHashes.TryGetValue(propertyInfo.Name, out oldPropHash);
					if (oldPropHash == null || newPropHash.IsExactlySameAs(oldPropHash))
					{ 
						continue;
					}
				
					changedProps.Add(propertyInfo);
				}
			}
		
			foreach (var fieldInfo in fieldInfos)
			{
				string fieldValue;
				var v = fieldInfo.GetValue(obj);
				if (fieldInfo.FieldType.IsClass && fieldInfo.FieldType.Name != "String")
				{
					fieldValue = System.Text.Json.JsonSerializer.Serialize(v);
				}
				else
				{
					fieldValue = v?.ToString() ?? string.Empty;
				}
				
				if (objHashes != null)
				{
					var newFieldHash = GetHash(Encoding.Default.GetBytes(fieldValue));
					string oldFieldHash;
				
					objHashes.TryGetValue(fieldInfo.Name, out oldFieldHash);
					if (oldFieldHash == null || newFieldHash.IsExactlySameAs(oldFieldHash))
					{ 
						continue;
					}
				
					changedFields.Add(fieldInfo);
				}
			}
		}

		return (changedProps.ToArray(), changedFields.ToArray());
	}
	
	/// <summary>
	/// Gets the object hash from the objects property values.
	/// </summary>
	/// <returns>An MD5 hash representing the object</returns>
	private static string GetObjectHash<T>(T obj, bool trackProperties) where T : IDirtyMonitoring
	{
		int objId = 0;
		if (trackProperties)
		{
			objId = obj.GetHashCode();
			ObjectValueHashStore.Add(objId, null);
		}
		
		string md5;
        try
        {
            var propertyInfos = obj.GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(IsDirtyMonitoringAttribute)) != null).ToList();
            var fieldInfos = obj.GetType().GetFields().Where(f => f.GetCustomAttribute(typeof(IsDirtyMonitoringAttribute)) != null).ToList();
            var sb = new StringBuilder();
            
            foreach (var propertyInfo in propertyInfos)
            {
	            string propValue;
	            object v = propertyInfo.GetValue(obj);
	            if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.Name != "String")
	            {
		            propValue = System.Text.Json.JsonSerializer.Serialize(v);
	            }
	            else
	            {
		            propValue = v?.ToString() ?? string.Empty;
	            }

	            if (trackProperties)
	            {
		            Dictionary<string, string> vals;
		            if (ObjectValueHashStore.TryGetValue(objId, out vals))
		            {
			            if (vals == null)
			            {
				            vals = new Dictionary<string, string>();
				            ObjectValueHashStore[objId] = vals;
			            }

			            vals.Add(propertyInfo.Name, GetHash(Encoding.Default.GetBytes(propValue)));
		            }
	            }
	            
	            sb.Append(propValue);
            }
            
            foreach (var fieldInfo in fieldInfos)
            {
	            string fieldValue;
	            object v = fieldInfo.GetValue(obj);
	            if (fieldInfo.FieldType.IsClass && fieldInfo.FieldType.Name != "String")
	            {
		            fieldValue = System.Text.Json.JsonSerializer.Serialize(v);
	            }
	            else
	            {
		            fieldValue = v?.ToString() ?? string.Empty;
	            }
	            
	            if (trackProperties)
	            {
		            Dictionary<string, string> vals;
		            if (ObjectValueHashStore.TryGetValue(objId, out vals))
		            {
			            if (vals == null)
			            {
				            vals = new Dictionary<string, string>();
				            ObjectValueHashStore[objId] = vals;
			            }
			            
			            vals.Add(fieldInfo.Name, GetHash(Encoding.Default.GetBytes(fieldValue)));
		            }
	            }
	            
	            sb.Append(fieldValue);
            }
            
            md5 = GetHash(Encoding.Default.GetBytes(sb.ToString()));
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
	private static string GetHash(byte[] buffer)
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