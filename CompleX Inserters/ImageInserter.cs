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
    public class ImageInserter:IInserter
    {
        public Guid ID
        {
            get { return new Guid("0FC9869C-5285-4BD9-A378-10B5489B8EDB");}
        }

        public string ServiceName
        {
            get { return "Image Inserter"; }
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
            get { return Texts.Image; }
        }

        public object GetObjectToInsert(InsertParameter param)
        {
            var t = new Tag(TagLanguage.HTML, "img");
            var inserter = new TagInserter();
            return inserter.GetObjectToInsert(new InsertParameter(t));
        }

        public Type GetParameterType()
        {
            return typeof(Tag);
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
            get { return Keys.I; }
        }
    }
}