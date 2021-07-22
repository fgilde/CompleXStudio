using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompleX_Library.Interfaces
{
    public delegate void OnException(object sender, Exception e);

    public interface IDesignable : IContentEdit
    {

        /// <summary>
        /// Event if Exception Occured
        /// </summary>
        event OnException ExceptionOccurred;


        /// <summary>
        /// Name of Company or Author
        /// </summary>
        string Author { get; }


        /// <summary>
        /// Return true if designer can work in Split mode.
        /// You need to Call event for OnCode change, and the prberty Source 
        /// will set every time if code has changed in CodeEditor
        /// </summary>
        bool SupportSplitMode { get; }

    }
}