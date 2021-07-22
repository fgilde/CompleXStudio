//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using CompleX_Library.Helper;

namespace CompleX_Library
{

    public static class ImageFunctions
    {

        private static Hashtable iconInfo;
        
        public static Hashtable IconFileInfo
        {
            get
            {
                if(iconInfo == null)
                    iconInfo = RegisteredFileType.GetFileTypeAndIcon();
                return iconInfo;
            }
        }

        public static Icon IconFromBase64String(string base64)
        {
            var memory = new MemoryStream(Convert.FromBase64String(base64));
            var result = new Icon(memory, new Size(32, 32));
            memory.Close();
            return result;
        }


        public static string IconToBase64String(Icon image)
        {
            var memory = new MemoryStream();
            image.Save(memory);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();

            return base64;
        }

        public static Icon GetAssociatedIconByExtension(string extension, bool isLarge)
        {
            var res = GetComplexSpecificExtensionIcon(extension);
            if (res != null)
                return res;
            try
            {
                string fileAndParam = (IconFileInfo[extension]).ToString();

                if (String.IsNullOrEmpty(fileAndParam))
                    return null;

                Icon icon = RegisteredFileType.ExtractIconFromFile(fileAndParam, isLarge);
                return icon;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static Icon GetComplexSpecificExtensionIcon(string extension)
        {
            if (extension.StartsWith("."))
                extension = extension.Substring(1);
            switch (extension)
            {
                case "webapp":
                    return Resource.iphone;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the Shell Icon for the given file..
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Icon GetFileIcon(string name)
        {
            return GetFileIcon(name, false);
        }

        /// <summary>
        /// Gets the Shell Icon for the given file...
        /// </summary>
        /// <param name="name">Path to file.</param>
        /// <param name="linkOverlay">Link Overlay</param>
        /// <returns>Icon</returns>
        public static Icon GetFileIcon(string name, bool linkOverlay)
        {
            var res = GetComplexSpecificExtensionIcon(Path.GetExtension(name));
            if (res != null)
                return res;

            //also you can use Icon.ExtractAssociatedIcon(fileName);
            var shfi = new Shell32.SHFILEINFO();
            uint flags = Shell32.SHGFI_ICON | Shell32.SHGFI_USEFILEATTRIBUTES;

            if (linkOverlay) flags += Shell32.SHGFI_LINKOVERLAY;
            flags += Shell32.SHGFI_SMALLICON; // include the small icon flag

            Shell32.SHGetFileInfo(name,
                Shell32.FILE_ATTRIBUTE_NORMAL,
                ref shfi,
                (uint)System.Runtime.InteropServices.Marshal.SizeOf(shfi),
                flags);


            // Copy (clone) the returned icon to a new object, thus allowing us 
            // to call DestroyIcon immediately
            var icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();
            User32.DestroyIcon(shfi.hIcon); // Cleanup
            return icon;
        }

        /// <summary>
        /// Ändert die Größe eines Bildes und setzt eine Hintergrundfarbe.
        /// </summary>
        /// <param name="Source">Originalbild als Image</param>
        /// <param name="Width">Neue Breite in Pixel</param>
        /// <param name="Height">Neue Höhe in Pixel</param>
        /// <param name="Absolut">Wenn true wird ein Image mit exakt den angegebenen Maßen erstellt. Das Bild wird mittig plaziert und mit der angegebenen Farbe gefüllt. Seitenverhältnisse des Bildes selbst bleiben erhalten. Wenn False dann wird entsprechend dem kleinem Faktor das neue Bild berechnet (neues Bild hat dann aber ggfs. NICHT die angegebenen Maße!)</param>
        /// <param name="Fill">Füllfarbe für Hintergrund (vorallem wichtig bei Absolut = true oder Transparenten Bildern (PNG)</param>
        /// <param name="MaxFactorX">Maximaler Vergrößerungsfaktor X-Achse. Wenn aus Width = 10 z.B. 1000 werden soll, so kann der Wert 2 ein maximales verdoppeln ermöglichen. Das Bild hat dann Width = 20. -1 bedeutet kein Mindestfaktor</param>
        /// <param name="MaxFactorY">Maximaler Vergrößerungsfaktor Y-Achse. Wenn aus Height = 10 z.B. 1000 werden soll, so kann der Wert 2 ein maximales verdoppeln ermöglichen. Das Bild hat dann Height = 20. -1 bedeutet kein Mindestfaktor</param>
        /// <returns>Bitmap / Image</returns>
        public static Image ResizeImage(Image Source, int Width, int Height, bool Absolut, Color Fill, double MaxFactorX, double MaxFactorY)
        {
            if (Source != null)
            {
                // Faktoren für X und Y Achse berechnen
                double dblFaktorX = (double)Width / (double)Source.Width;
                if (dblFaktorX > MaxFactorX & MaxFactorX > -1)
                {
                    Width = (int)((double)Source.Width * MaxFactorX);
                    dblFaktorX = MaxFactorX;
                }

                double dblFaxtorY = (double)Height / (double)Source.Height;
                if (dblFaxtorY > MaxFactorY & MaxFactorY > -1)
                {
                    Height = (int)((double)Source.Height * MaxFactorY);
                    dblFaxtorY = MaxFactorY;
                }

                // kleinern Faktor benutzen
                double dblFaktorUse;
                if (dblFaktorX < dblFaxtorY)
                    dblFaktorUse = dblFaktorX;
                else
                    dblFaktorUse = dblFaxtorY;

                int intNewX = (int)((double)Source.Width * dblFaktorUse);
                int intNewY = (int)((double)Source.Height * dblFaktorUse);

                Bitmap objNewImage;
                int intPosX, intPosY;

                if (Absolut)
                {
                    objNewImage = new Bitmap(Width, Height);
                    intPosX = (Width - intNewX) / 2;
                    intPosY = (Height - intNewY) / 2;
                }
                else
                {
                    objNewImage = new Bitmap(intNewX, intNewY);
                    intPosX = 0;
                    intPosY = 0;
                }

                using (Graphics objGfx = Graphics.FromImage(objNewImage))
                {
                    objGfx.FillRectangle(new SolidBrush(Fill), 0, 0, objNewImage.Width, objNewImage.Height);
                    objGfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGfx.DrawImage(Source, new Rectangle(intPosX, intPosY, intNewX, intNewY));
                }
                return objNewImage;
            }
            else
                return null;
        }

        /// <summary>
        /// Crops the and resize image.
        /// </summary>
        /// <param name="img">The image</param>
        /// <param name="targetWidth">Width of the target.</param>
        /// <param name="targetHeight">Height of the target.</param>
        /// <param name="x1">The position x1.</param>
        /// <param name="y1">The position y1.</param>
        /// <param name="x2">The position x2.</param>
        /// <param name="y2">The position y2.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>MemoryStream of the cropped and resized image.</returns>
        public static MemoryStream CropAndResizeImage(Image img, int targetWidth, int targetHeight, int x1, int y1, int x2, int y2, ImageFormat imageFormat)
        {
            var bmp = new Bitmap(targetWidth, targetHeight);
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            int width = x2 - x1;
            int height = y2 - y1;

            g.DrawImage(img, new Rectangle(0, 0, targetWidth, targetHeight), x1, y1, width, height, GraphicsUnit.Pixel);

            var memStream = new MemoryStream();
            bmp.Save(memStream, imageFormat);
            return memStream;
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="img">The image</param>
        /// <param name="targetWidth">Width of the target.</param>
        /// <param name="targetHeight">Height of the target.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>MemoryStream of the resized image.</returns>
        public static MemoryStream ResizeImage(Image img, int targetWidth, int targetHeight, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            return CropAndResizeImage(img, targetWidth, targetHeight, 0, 0, img.Width, img.Height, imageFormat);
        }

        /// <summary>
        /// Crops the image.
        /// </summary>
        /// <param name="img">The image</param>
        /// <param name="x1">The position x1.</param>
        /// <param name="y1">The position y1.</param>
        /// <param name="x2">The position x2.</param>
        /// <param name="y2">The position y2.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>MemoryStream of the cropped image.</returns>
        public static MemoryStream CropImage(Image img, int x1, int y1, int x2, int y2, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            return CropAndResizeImage(img, x2 - x1, y2 - y1, x1, y1, x2, y2, imageFormat);
        }

    }
}
