using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;


namespace CompleX_Library
{
    /// <summary>
    /// Zusammenfassung mehrfach verwendeter Methoden für's GUI.
    /// </summary>
    public static class ViewUtility
    {

        private const int Style = -16;
        private const int ExtStyle = -20;

        private const int MaximizeBox = 0x10000;
        private const int MinimizeBox = 0x20000;
        private const int CloseBox = 0x00080000;

        [DllImport("user32.dll")]
        internal static extern int GetWindowLong(IntPtr hWnd, int index);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int index, int newLong);

        /// <summary>
        /// Setzt die Sichtbarkeit der WindowButtons (minimieren, schließen etc)
        /// </summary>
        public static void SetWindowButtons(this Window window, bool minimizeButtonVisible, bool maximizeButtonVisible, bool closeButtonVisible)
        {
            IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
            var style = GetWindowLong(hWnd, Style);

            if (maximizeButtonVisible)
                style |= MaximizeBox;
            else
                style &= ~MaximizeBox;

            if (minimizeButtonVisible)
                style |= MinimizeBox;
            else
                style &= ~MinimizeBox;

            if (closeButtonVisible)
                style |= CloseBox;
            else
                style &= ~CloseBox;

            SetWindowLong(hWnd, Style, style);
        }

        /// <summary>
        /// Konvertiert ein System.Drawing.Bitmap (Resources .Net 2.0) in einen ImageSource (WPF)
        /// </summary>
        /// <param name="source">Das Bitmap das konvertiert werden soll</param>
        /// <returns></returns>
        public static ImageSource ToImageSource(this Bitmap source)
        {
            if (source == null)
                return null;

            IntPtr hbitmap = source.GetHbitmap();
            BitmapSource bitmap = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hbitmap);
            return bitmap;
        }

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Konvertiert ein System.Drawing.Icon (Resources .Net 2.0) in einen ImageSource (WPF)
        /// </summary>
        /// <param name="source">Das Bitmap das konvertiert werden soll</param>
        /// <returns></returns>
        public static ImageSource ToImageSource(this Icon source)
        {
            var memoryStream = new MemoryStream();
            source.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            ImageSource result = BitmapFrame.Create(memoryStream);
            return result;
        }

        /// <summary>
        /// Erstellt <see cref="System.Windows.Media.Color"/> aus einem <see cref="uint"/>
        /// </summary>
        /// <param name="argb">ARGB Wert</param>
        public static Color FromUInt(uint argb)
        {
            var color = new Color
            {
                A = (byte)((argb & -16777216) >> 24),
                R = (byte)((argb & 16711680) >> 16),
                G = (byte)((argb & 65280) >> 8),
                B = (byte)(argb & 255)
            };
            return color;
        }

        /// <summary>
        /// Konvertiert WinForms Point in WPF Point
        /// </summary>
        /// <param name="point">WPF point</param>
        public static System.Windows.Point ToPoint(this System.Drawing.Point point)
        {
            return new System.Windows.Point(point.X, point.Y);
        }

        /// <summary>
        /// Finds the visual parent.
        /// </summary>
        /// <typeparam name="TParentItem">The type of the parent item.</typeparam>
        /// <param name="obj">The obj.</param>
        public static TParentItem FindVisualParent<TParentItem>(DependencyObject obj) where TParentItem : DependencyObject
        {
            DependencyObject current = VisualTreeHelper.GetParent(obj);
            while (current != null && !(current is TParentItem))
                current = VisualTreeHelper.GetParent(current);
            return (TParentItem)current;
        }

    }
}
