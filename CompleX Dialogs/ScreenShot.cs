using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows;
using Size = System.Drawing.Size;

namespace CompleX.Presentation.Controls
{
	/// <summary>
	/// Makes screenshot.
	/// </summary>
	internal static class ScreenShot
	{
		/// <summary>
		/// Captures the specified window.
		/// </summary>
		/// <param name="window">The window.</param>
		/// <param name="fileName">Name of the file.</param>
		public static void Capture(Window window, string fileName)
		{
			Contract.Requires(window != null);
			Contract.Requires(!string.IsNullOrWhiteSpace(fileName));

			Bitmap bitmap = new Bitmap((int)window.ActualWidth, (int)window.ActualHeight);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen((int)window.Left, (int)window.Top, 0, 0, new Size(bitmap.Width, bitmap.Height));
			bitmap.Save(fileName);
		}
	}
}