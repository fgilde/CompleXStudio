//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CompleX.Dialogs;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Types;

namespace CompleX_Inserters
{
    [Export(typeof(IInserter))]
    public class TagInserter:IInserter
    {
        #region Implementation of IEquatable<IHostedService>

        public bool Equals(IHostedService other)
        {
            return ID.Equals(other.ID);
        }

        #endregion

        #region Implementation of IInserter

        /// <summary>
        /// Name of object (example "Image")
        /// </summary>
        public string NameOfObjectToInsert
        {
            get { return "Tag"; }
        }

        public object GetObjectToInsert(InsertParameter param)
        {
            if (param.HasSomeData && param.Data is Tag)
            {
                var tag = (Tag) param.Data;
                var editorDlg = new TagEditorDlg(tag);
                if (editorDlg.ShowDialog() == DialogResult.OK)
                    return tag;
            }
            else
            {
                Tag tag;
                if (TagSelectorDlg.Execute(out tag))
                {
                    var editorDlg = new TagEditorDlg(tag);
                    if (editorDlg.ShowDialog() == DialogResult.OK)
                        return tag;    
                }          
            }
            return null;
        }

        /// <summary>
        /// Should return the type of requested parameter
        /// </summary>
        /// <returns></returns>
        public Type GetParameterType()
        {
            return typeof(Tag);
        }

        /// <summary>
        /// Image for MenuIcon and ToolBox entry
        /// </summary>
        /// <value>The image.</value>
        public Image Image
        {
            get { return Resources.htmltag;}
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
            get { return Keys.T; }
        }

        #endregion

        #region Implementation of IHostedService

        /// <summary>
        /// The Unique ID of this service 
        /// </summary>
        public Guid ID
        {
            get
            {
                return new Guid("E58BFD08-A4EC-4F99-B733-AB0838CE4216");
            }
        } 

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        public string ServiceName { get { return "CompleX TagInserter"; } }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return FilenameExtensions.ExtensionsWebFiles; }   
        }

        /// <summary>
        /// Function is called if service is added 
        /// return true if everything ok otherwise service would not loaded
        /// </summary>
        public bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// Function is called before service will added 
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        public Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
            
        }

        #endregion
    }
}
