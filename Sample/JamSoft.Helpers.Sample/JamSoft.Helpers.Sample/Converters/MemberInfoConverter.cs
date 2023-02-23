using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Avalonia.Data.Converters;

namespace JamSoft.Helpers.Sample.Converters;

public class MemberInfoConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value != null)
		{
			MemberInfo[] coll = (MemberInfo[])value;
			return string.Join(Environment.NewLine, coll.Select(x => x.Name));
		}

		return string.Empty;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return null;
	}
}