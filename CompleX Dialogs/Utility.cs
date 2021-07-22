using System.ComponentModel;
using System.Windows;

namespace CompleX.Presentation.Controls
{
	internal static class Utility
	{
		private static bool? isInDesignMode;

		public static bool IsInDesignMode
		{
			get
			{
				if (!isInDesignMode.HasValue)
				{
					isInDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty,
						typeof(FrameworkElement)).Metadata.DefaultValue;
				}
				return isInDesignMode ?? false;
			}
		}
	}
}
