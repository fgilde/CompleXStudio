using System;
using System.Globalization;
using System.Windows.Data;

namespace CompleX.Presentation.Controls.Converters
{
	internal class TruncateStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				string message = value as string;
				if (!string.IsNullOrEmpty(message) && message.Length > 500)
					return message.Substring(0, 500) + "...";
				return message;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
