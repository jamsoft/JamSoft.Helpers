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
	private static readonly Dictionary<Type, Tuple<IEnumerable<PropertyInfo>, IEnumerable<FieldInfo>>> TypeInfoCache = new();
	
	/// <summary>
	/// Validates properties and fields decorated with <see cref="IsDirtyMonitoringAttribute"/> on whole instances for dirtiness. If any monitored value has changed, the IsDirty property will be set to True 
	/// </summary>
	/// <param name="instance">The instance to be validated</param>
	/// <param name="trackProperties"></param>
	/// <typeparam name="T">The object type</typeparam>
	/// <returns>The sample annotated object instance</returns>
	/// <remarks>Validate the clean object in order to calculate the clean hash value for the object instance. Subsequent calls will detect if the contents has changes between validations</remarks>
	public static T Validate<T>(T instance, bool trackProperties = false) where T : IDirtyMonitoring, new()
	{
		if (instance != null)
		{
			if (trackProperties)
			{
				StopTrackingObject(instance);
			}
		
			if (string.IsNullOrWhiteSpace(instance.Hash))
			{
				instance.Hash = GetObjectHash(instance, trackProperties);
				instance.IsDirty = false;
			}
			else
			{
				var newHash = GetObjectHash(instance, trackProperties);
				instance.IsDirty = !instance.Hash.IsExactlySameAs(newHash);
			}
		}
		
		return instance;
	}
	
	/// <summary>
	/// Checks individual values within properties and fields and returns a list of each with changes
	/// </summary>
	/// <remarks>For a call to this method to work, the object must have been previously validated with by calling the Validate method with trackProperties set to true</remarks>
	/// <param name="instance">The object instance to validate</param>
	/// <typeparam name="T">The object type</typeparam>
	/// <returns>Two arrays, one of <see cref="PropertyInfo"/> objects and one of <see cref="FieldInfo"/> objects</returns>
	public static (PropertyInfo[] properties, FieldInfo[] fields) ValidatePropertiesAndFields<T>(T instance) where T : IDirtyMonitoring, new()
	{
		List<PropertyInfo> changedProps = new List<PropertyInfo>();
		List<FieldInfo> changedFields = new List<FieldInfo>();
		
		if (instance != null && !instance.Hash.IsExactlySameAs(GetObjectHash(instance, false)))
		{
			var (propertyInfos, fieldInfos) = GetTypeInfo(instance);
			var objId = instance.GetHashCode();

			ObjectValueHashStore.TryGetValue(objId, out var objHashes);
		
			foreach (var propertyInfo in propertyInfos)
			{
				var propValue = GetPropertyValue(instance, propertyInfo);
				if (objHashes != null)
				{
					var newPropHash = GetHash(Encoding.Default.GetBytes(propValue));
					objHashes.TryGetValue(propertyInfo.Name, out var oldPropHash);
					if (oldPropHash == null || newPropHash.IsExactlySameAs(oldPropHash))
					{ 
						continue;
					}
				
					changedProps.Add(propertyInfo);
				}
			}
		
			foreach (var fieldInfo in fieldInfos)
			{
				var fieldValue = GetFieldValue(instance, fieldInfo);
				if (objHashes != null)
				{
					var newFieldHash = GetHash(Encoding.Default.GetBytes(fieldValue));
					objHashes.TryGetValue(fieldInfo.Name, out var oldFieldHash);
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
	/// Stops tracking an object by clearing down any stored information and resetting the properties
	/// </summary>
	/// <param name="instance">The object to stop tracking</param>
	/// <typeparam name="T">The object type</typeparam>
	public static void StopTrackingObject<T>(T instance) where T : IDirtyMonitoring, new()
	{
		if (instance == null) return;
		
		var objId = instance.GetHashCode();
		instance.Hash = null;
		instance.IsDirty = false;
		if (ObjectValueHashStore.ContainsKey(objId))
		{
			ObjectValueHashStore.Remove(objId);
		}
	}
	
	/// <summary>
	/// Gets the object hash from the objects property values.
	/// </summary>
	/// <returns>An MD5 hash representing the object</returns>
	private static string GetObjectHash<T>(T instance, bool trackProperties) where T : IDirtyMonitoring
	{
		int objId = 0;
		if (trackProperties)
		{
			objId = instance.GetHashCode();
			ObjectValueHashStore.Add(objId, null);
		}
		
		string md5;
        try
        {
            var (propertyInfos, fieldInfos) = GetTypeInfo(instance);
            var sb = new StringBuilder();
            
            foreach (var propertyInfo in propertyInfos)
            {
	            var propValue = GetPropertyValue(instance, propertyInfo);

	            if (trackProperties)
	            {
		            if (ObjectValueHashStore.TryGetValue(objId, out var hashes))
		            {
			            if (hashes == null)
			            {
				            hashes = new Dictionary<string, string>();
				            ObjectValueHashStore[objId] = hashes;
			            }

			            hashes.Add(propertyInfo.Name, GetHash(Encoding.Default.GetBytes(propValue)));
		            }
	            }
	            
	            sb.Append(propValue);
            }
            
            foreach (var fieldInfo in fieldInfos)
            {
	            var fieldValue = GetFieldValue(instance, fieldInfo);
	            
	            if (trackProperties)
	            {
		            if (ObjectValueHashStore.TryGetValue(objId, out var hashes))
		            {
			            if (hashes == null)
			            {
				            hashes = new Dictionary<string, string>();
				            ObjectValueHashStore[objId] = hashes;
			            }
			            
			            hashes.Add(fieldInfo.Name, GetHash(Encoding.Default.GetBytes(fieldValue)));
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

	private static string GetPropertyValue<T>(T instance, PropertyInfo propertyInfo) where T : IDirtyMonitoring
	{
		string propValue;
		object v = propertyInfo.GetValue(instance);
		if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.Name != "String")
		{
			propValue = System.Text.Json.JsonSerializer.Serialize(v);
		}
		else
		{
			propValue = v?.ToString() ?? string.Empty;
		}

		return propValue;
	}
	
	private static string GetFieldValue<T>(T instance, FieldInfo fieldInfo) where T : IDirtyMonitoring
	{
		string fieldValue;
		var v = fieldInfo.GetValue(instance);
		if (fieldInfo.FieldType.IsClass && fieldInfo.FieldType.Name != "String")
		{
			fieldValue = System.Text.Json.JsonSerializer.Serialize(v);
		}
		else
		{
			fieldValue = v?.ToString() ?? string.Empty;
		}

		return fieldValue;
	}

	/// <summary>
	/// Gets the MD5 sum from the buffer byte data.
	/// </summary>
	/// <param name="buffer">The buffer.</param>
	/// <returns>a string MD5 value</returns>
	private static string GetHash(byte[] buffer)
	{
		using var hasher = MD5.Create();
		return BitConverter.ToString(hasher.ComputeHash(buffer));
	}

	private static (IEnumerable<PropertyInfo> propInfos, IEnumerable<FieldInfo> fieldInfos) GetTypeInfo<T>(T instance)
	{
		Type t = instance.GetType();
		TypeInfoCache.TryGetValue(t, out Tuple<IEnumerable<PropertyInfo>, IEnumerable<FieldInfo>> typeInfo);
		if (typeInfo != null)
			return (typeInfo.Item1, typeInfo.Item2);
		
		var propertyInfos = t.GetProperties().Where(p => p.GetCustomAttribute(typeof(IsDirtyMonitoringAttribute)) != null);
		var fieldInfos = t.GetFields().Where(f => f.GetCustomAttribute(typeof(IsDirtyMonitoringAttribute)) != null);
		var tuple = new Tuple<IEnumerable<PropertyInfo>, IEnumerable<FieldInfo>>(propertyInfos, fieldInfos);
		TypeInfoCache.Add(t, tuple);
		return (tuple.Item1, tuple.Item2);
	}
}