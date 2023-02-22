using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace JamSoft.Helpers.Sample.Converters;

public class IsDirtyColorConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value != null)
		{
			switch (value)
			{
				case true:
					return new SolidColorBrush(Color.Parse("#FF8D8D"));
				case false:
					return new SolidColorBrush(Color.Parse("#B8FFB8"));
			}
		}

		return new SolidColorBrush(Color.Parse("#FF8D8D"));
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}