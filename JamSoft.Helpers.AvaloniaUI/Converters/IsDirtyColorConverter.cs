using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace JamSoft.Helpers.AvaloniaUI.Converters;

/// <summary>
/// A binding helper for IsDirty UI Feedback
/// </summary>
public class IsDirtyColorConverter : IValueConverter
{
	/// <summary>
	/// The IsDirty=False color
	/// </summary>
	public SolidColorBrush FalseColor { get; set; } = new SolidColorBrush(Color.FromRgb(184, 255, 184));

	/// <summary>
	/// The IsDirty=True color
	/// </summary>
	public SolidColorBrush TrueColor { get; set; } = new SolidColorBrush(Color.FromRgb(255, 141, 141));
	
	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value != null)
		{
			switch (value)
			{
				case true:
					return TrueColor;
				case false:
					return FalseColor;
			}
		}

		return FalseColor;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return null;
	}
}