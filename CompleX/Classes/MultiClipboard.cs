using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;


namespace CompleX.Classes
{
    class MultiClipboard
    {
        // Event
        /// <summary>
        /// Stellt die Methode dar, die ein Ereigniss behandelt.
        /// </summary>
        internal delegate void ClipBoardChangHandler(Object sender, ClipBoardChangEventArgs ex);
        /// <summary>
        /// Tritt auf sobald sich die Zwischenablage ändert.
        /// </summary>
        internal event ClipBoardChangHandler clipBoardChanged;

        // Api Call
        [DllImport("user32")]
        private extern static IntPtr SetClipboardViewer(IntPtr hWnd);
        [DllImport("user32")]
        private extern static int ChangeClipboardChain(IntPtr hWnd, IntPtr hWndNext);

        // Fields
        private Form formForMessageLoop;
        private IntPtr nextViewer = IntPtr.Zero;
        private IntPtr currentViewer = IntPtr.Zero;

        /// <summary>
        /// Legt die Form fest an die, die MessageLoop gebunden werden soll.
        /// </summary>
        internal Form MainForm
        {
            set
            {
                formForMessageLoop = value;
                // Form Anmelden
                RegisterForm(formForMessageLoop);
            }
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der MultiClipboard Klasse.
        /// </summary>
        internal MultiClipboard()
        {
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der MultiClipboard Klasse
        /// </summary>
        /// <param name="mainForm">Legt die Form fest an die, die MessageLoop gebunden werden soll.</param>
        internal MultiClipboard(Form mainForm)
        {
            formForMessageLoop = mainForm;
            // Form anmelden
            RegisterForm(mainForm);
        }
        /// <summary>
        /// Messageloop an Form Binden, sowie Event anbinden. 
        /// </summary>
        /// <param name="mainForm">Legt die Form fest an die, die MessageLoop gebunden werden soll.</param>
        private void RegisterForm(Form mainForm)
        {
            MessageLoopMainForm messageLoopMainForm = new MessageLoopMainForm(mainForm);

            RegisterViewer(mainForm);

            messageLoopMainForm.NextClipBoardViewer = nextViewer;
            //bei aufruf sofort an per Event weiterleiten
            messageLoopMainForm.clipBoardBoardChanged += new MessageLoopMainForm.ClipBoardChangEventHandler
                (delegate(Message m) { if (clipBoardChanged != null) clipBoardChanged(this, new ClipBoardChangEventArgs(Clipboard.GetDataObject())); });
        }
        /// <summary>
        /// Diesen Viewer an der Message Loop anmelden
        /// </summary>
        /// <param name="mainForm"></param>
        private void RegisterViewer(Form mainForm)
        {
            nextViewer = SetClipboardViewer(mainForm.Handle);
            currentViewer = mainForm.Handle;
        }
        /// <summary>
        /// Diesen Viewer wieder aus dem Message Loop wieder herrausnehmen
        /// </summary>
        private void DeRegisterViwer()
        {
            if (nextViewer != IntPtr.Zero)
            {
                ChangeClipboardChain(currentViewer, nextViewer);
                nextViewer = IntPtr.Zero;
                currentViewer = IntPtr.Zero;
            }
        }
        /// <summary>
        /// Destructor dieser Klasse
        /// </summary>
        ~MultiClipboard()
        {
            DeRegisterViwer();
        }
    }

    internal class MessageLoopMainForm : NativeWindow
    {
        // delegate
        internal delegate void ClipBoardChangEventHandler(Message m);
        internal ClipBoardChangEventHandler clipBoardBoardChanged;

        // Api Call
        [DllImport("user32")]
        private extern static int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        // Konstanten
        /// <summary>
        /// Message wenn sich am Clipboard etwas ändert
        /// </summary>
        private const int WM_DRAWCLIPBOARD = 0x308;
        /// <summary>
        /// Message sobald sich an den anderen Viewern etwas ändert
        /// </summary>
        private const int WM_CHANGECBCHAIN = 0x30D;

        private IntPtr nextViewer;
        internal IntPtr NextClipBoardViewer
        {
            set
            {
                nextViewer = value;
            }
        }

        internal MessageLoopMainForm(Form mainForm)
        {
            // an die jeweiligen Form anhängen
            AssignHandle(mainForm.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_CHANGECBCHAIN:
                    {
                        // Nächsten Viewer ermitteln
                        nextViewer = m.LParam;
                        if (nextViewer != IntPtr.Zero)
                        {
                            // Message an den nächsten Viewer weiterleiten
                            SendMessage(nextViewer, m.Msg, m.WParam, m.LParam);
                        }
                        break;
                    }
                case WM_DRAWCLIPBOARD:
                    {
                        if (clipBoardBoardChanged != null)
                            clipBoardBoardChanged(m);
                        // Message an den nächsten Viewer weiterleiten
                        if (nextViewer != IntPtr.Zero)
                        {
                            SendMessage(nextViewer, m.Msg, m.WParam, m.LParam);
                        }
                        m.Result = IntPtr.Zero;
                        break;
                    }
                default:
                    {
                        // An die normale WndProc weiterleiten
                        base.WndProc(ref m);
                        break;
                    }
            }
        }
    }
    /// <summary>
    /// Stellt die daten des Ereignisses zur Verfügung
    /// </summary>
    public class ClipBoardChangEventArgs : EventArgs
    {
        private IDataObject clipBoardObject;
        /// <summary>
        /// Initialisiert eine neue Instanz der ClipBoardChangEventArgs Klasse.
        /// </summary>
        public ClipBoardChangEventArgs(IDataObject clipBoardObject) { this.clipBoardObject = clipBoardObject; }
        /// <summary>
        /// Die enthaltenen Daten des Clipboards, zum Zeitpunkt an dem das Ereigniss aufgetrten ist.
        /// </summary>
        public IDataObject ClipBoardObject { get { return clipBoardObject; } }
    } 

}
