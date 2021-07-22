using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CompleX.Helper;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Types;

namespace CompleX_Inserters
{
    [Export(typeof(IInserter))]
    public class MediaInserter:IInserter
    {
        public Guid ID
        {
            get { return new Guid("5857D98B-6A4E-433C-892B-CEE26453121F");}
        }

        public string ServiceName
        {
            get { return "Media Inserter"; }
        }

        public IEnumerable<string> SupportedFileExtensions
        {
            get { return FilenameExtensions.ExtensionsWebFiles; }
        }

        public bool Initialize()
        {
            return true;
        }

        public Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public string NameOfObjectToInsert
        {
            get { return Texts.MediaFile; }
        }

        public object GetObjectToInsert(InsertParameter param)
        {
            var dlg = new OpenFileDialog();
            if(dlg.ShowDialog()== DialogResult.OK)
            {
                string fileName = dlg.FileName.ToRelativePathOrUri();
                return fileName;
            }
            return null;
        }

        public Type GetParameterType()
        {
            return typeof(object);
        }

        public Image Image
        {
            get { return Resources.einsetzen_16; }
        }

        /// <summary>
        /// ShortCut for Access this directly
        /// </summary>
        public Keys ShortCutKey1
        {
            get { return Keys.Control | Keys.I; }
        }

        /// <summary>
        /// ShortCut2
        /// </summary>
        public Keys ShortCutKey2
        {
            get { return Keys.M; }
        }
    }
}