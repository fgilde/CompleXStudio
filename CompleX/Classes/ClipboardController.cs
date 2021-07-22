using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompleX.Classes
{
    public class ClipboardController
    {
        private Dictionary<Form,MultiClipboard> internalMultiClipboardList;
        private List<string> clipboardStringList;
        private List<IDataObject> clipboardDataList;
        private int clipboardIndex;
        private static object syncObject = new object();
        private static ClipboardController current;

        public static ClipboardController Current
        {
            get
            {
                lock (syncObject)
                {
                    if(current == null)
                       current = new ClipboardController();
                    return current;
                }
            }
        }

        public List<string> ClipboardStringList
        {
            get { return clipboardStringList; }
        }

        public List<IDataObject> ClipboardDataList
        {
            get { return clipboardDataList; }
        }

        public int ClipboardIndex
        {
            get { return clipboardIndex; }
        }

        internal ClipboardController()
        {
            Init();
        }

        internal ClipboardController(Form form)
        {
            Init();
            AddForm(form);
        }

        private void Init()
        {
            internalMultiClipboardList = new Dictionary<Form, MultiClipboard>();
            clipboardStringList = new List<string>();
            clipboardDataList = new List<IDataObject>();
        }

        public void AddForm(Form form)
        {
            if (!internalMultiClipboardList.ContainsKey(form))
            {
                var multiClipboard = new MultiClipboard(form);
                internalMultiClipboardList.Add(form, multiClipboard);
                multiClipboard.clipBoardChanged += (MultiClipboardClipBoardChanged);
            }
        }
        public bool RemoveForm(Form form)
        {
            if (internalMultiClipboardList.ContainsKey(form))
            {
                var multiClipboard = internalMultiClipboardList[form];
                if (multiClipboard != null)
                {  
                    multiClipboard.clipBoardChanged -= (MultiClipboardClipBoardChanged);
                    return internalMultiClipboardList.Remove(form);
                }
            }
            return false;
        }

        private void MultiClipboardClipBoardChanged(object sender, ClipBoardChangEventArgs ex)
        {
            // Event wenn etwas zu zwischenablage hinzugefügt wurde 
            string s = GetString(ex.ClipBoardObject);
            clipboardDataList.Add(ex.ClipBoardObject);
            if (!String.IsNullOrEmpty(s) && !clipboardStringList.Contains(s))
            {
                clipboardStringList.Add(s);
                clipboardIndex = clipboardStringList.Count - 1;
            }

            try
            {
                CompleX.ComplexEvents.Default.InvokeClipboardChanged(ex);
            }
            catch {}

        }


        public static string GetString(IDataObject dataObject)
        {
            //Gibt den Inhalt aus dem Clipboard zurück
            // ex string s = GetStringFromClipboardDataObject(Clipboard.GetDataObject());
            string strTextFromClipboard = string.Empty;

            if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
                string strChar = (String)dataObject.GetData(DataFormats.UnicodeText);
                strTextFromClipboard = strChar;
            }
            return strTextFromClipboard;
        }

    }  

}
