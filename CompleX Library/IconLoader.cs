using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace CompleX_Library
{
    public class IconLoader : IDisposable
    {
        private readonly List<IconEntry> icoEntrys;
        private readonly Header icoHeader;

        private MemoryStream icoStream;

        /// <summary>
        /// creates a new instance of <see cref="IconLoader"/>
        /// </summary>
        /// <param name="filename">icon file name</param>
        public IconLoader(string filename)
        {
            icoEntrys = new List<IconEntry>();

            if (LoadFromFile(filename))
            {
                icoHeader = new Header(icoStream);

                // Read the icons
                for (int counter = 0; counter < icoHeader.Count; counter++)
                {
                    var entry = new IconEntry(icoStream);
                    icoEntrys.Add(entry);
                }
            }
        }

        private List<IconEntry> Infos
        {
            get { return icoEntrys; }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (icoStream != null)
            {
                icoStream.Close();
                icoStream.Dispose();
                icoStream = null;
            }
        }

        #endregion

        public List<IconEntry> GetEntries()
        {
            var result = new List<IconEntry>();
            for (int i = 0; i < Infos.Count; i++)
            {
                var entry = new IconEntry(Infos[i], Create(i));
                result.Add(entry);
            }
            return result;
        }


        //--------------------------------------------------------------------------
        // Main class
        //--------------------------------------------------------------------------

        //--------------------------------------------------------------------------
        // Function: LoadFromFile
        //  Purpose: Loads the icon file into the memory stream
        //
        private bool LoadFromFile(string filename)
        {
            try
            {
                using (var icoFile = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    var icoArray = new byte[icoFile.Length];
                    icoFile.Read(icoArray, 0, (int) icoFile.Length);
                    icoStream = new MemoryStream(icoArray);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //--------------------------------------------------------------------------
        // Function: Create
        //  Purpose: Loads the icon file into the memory stream
        //
        private Icon Create(int index)
        {
            IconEntry thisIcon = icoEntrys[index];
            var memoryStream = new MemoryStream();
            var writer = new BinaryWriter(memoryStream);

            // New Values
            const Int16 newNumber = 1;
            const Int32 newOffset = 22;

            // Write it
            writer.Write(icoHeader.Reserved);
            writer.Write(icoHeader.Type);
            writer.Write(newNumber);
            writer.Write((byte) thisIcon.Width);
            writer.Write((byte) thisIcon.Height);
            writer.Write(thisIcon.ColorCount);
            writer.Write(thisIcon.Reserved);
            writer.Write(thisIcon.Planes);
            writer.Write(thisIcon.Resolution);
            writer.Write(thisIcon.BytesInRes);
            writer.Write(newOffset);

            // Grab the icon
            var tmpBuffer = new byte[thisIcon.BytesInRes];
            icoStream.Position = thisIcon.ImageOffset;
            icoStream.Read(tmpBuffer, 0, thisIcon.BytesInRes);
            writer.Write(tmpBuffer);

            // Finish up
            writer.Flush();
            memoryStream.Position = 0;
            return new Icon(memoryStream, thisIcon.Width, thisIcon.Height);
        }

        #region Nested type: Header

        private class Header
        {
            public readonly Int16 Count;
            public readonly Int16 Reserved;
            public readonly Int16 Type;

            public Header(Stream icoStream)
            {
                var icoFile = new BinaryReader(icoStream);

                Reserved = icoFile.ReadInt16();
                Type = icoFile.ReadInt16();
                Count = icoFile.ReadInt16();
            }
        }

        #endregion

        #region Nested type: IconEntry

        public class IconEntry
        {
            public IconEntry(MemoryStream icoStream)
            {
                var icoFile = new BinaryReader(icoStream);

                Width = icoFile.ReadByte();
                Height = icoFile.ReadByte();
                ColorCount = icoFile.ReadByte();
                Reserved = icoFile.ReadByte();
                Planes = icoFile.ReadInt16();
                Resolution = icoFile.ReadInt16();
                BytesInRes = icoFile.ReadInt32();
                ImageOffset = icoFile.ReadInt32();
                if (Width == 0 && BytesInRes > 0)
                    Width = 256;
                if (Height == 0 && BytesInRes > 0)
                    Height = 256;

            }

            public IconEntry(IconEntry source, Icon icon)
            {
                Icon = icon;

                Width = source.Width;
                Height = source.Height;
                ColorCount = source.ColorCount;
                Reserved = source.Reserved;
                Planes = source.Planes;
                Resolution = source.Resolution;
                BytesInRes = source.BytesInRes;
                ImageOffset = source.ImageOffset;
                Width = Icon.Width;
                Height = Icon.Height;
            }

            public Int16 Resolution { get; private set; }
            public Int32 BytesInRes { get; private set; }
            public byte ColorCount { get; private set; }
            public int Height { get; private set; }
            public Int32 ImageOffset { get; private set; }
            public Int16 Planes { get; private set; }
            public byte Reserved { get; private set; }
            public int Width { get; private set; }
            public Icon Icon { get; private set; }

            public override string ToString()
            {
                return
                    string.Format(
                        "Resolution: {0}, BytesInRes: {1}, ColorCount: {2}, Height: {3}, ImageOffset: {4}, Planes: {5}, Reserved: {6}, Width: {7}",
                        Resolution, BytesInRes, ColorCount, Height, ImageOffset, Planes, Reserved, Width);
            }

        }

        #endregion
    }
}