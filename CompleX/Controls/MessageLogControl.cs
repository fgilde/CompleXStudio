//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CompleX.Helper;
using CompleX.ServiceModel;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;

namespace CompleX.Controls
{
    ///<summary>
    /// This control provides 
    ///</summary>
    public partial class MessageLogControl : HostedControl
    {
        private readonly string logEntryRow = "{0}\t{1}\t{2}"+Environment.NewLine;      
        private IMessageLog messageLog;
        private bool initializing;


        /// <summary>
        /// Constructor.
        /// </summary>
        public MessageLogControl()
        {
            InitializeComponent();
            SetCheckBoxes();
        }

        public void SetCheckBoxes()
        {
            checkBoxTime.Checked = Settings.Get(Const.SettingShowLogFromTodayOnly, true);
        }

        ///<summary>
        /// Initializes the control.
        ///</summary>
        ///<param name="log">The Message log.</param>
        public void Initialize(IMessageLog log)
        {
            Initialize(log,LogType.None);
        }

        ///<summary>
        /// Initializes the control.
        ///</summary>
        ///<param name="log"></param>
        ///<param name="logtype"></param>
        public void Initialize(IMessageLog log, LogType logtype)
        {
            if(log == null)
                return;
            initializing = true;
  
            comboBoxLogType.Items.Clear();
            foreach (LogType logType in Enum.GetValues(typeof(LogType)))
                comboBoxLogType.Items.Add(LogEntry.GetLogType(logType));

            comboBoxLogType.SelectedIndex = (int) logtype;

            messageLog = log;
            dataSetMessageLog.Log.BeginLoadData();
            try
            {
                dataSetMessageLog.Log.Clear();

                var logEntries = log.History;
                foreach (LogEntry entry in logEntries)
                {
                    if (logtype == LogType.None || logtype == entry.LogType)
                    {
                        if (checkBoxTime.Checked)
                        {                          
                            if(entry.Time.Date == DateTime.Now.Date)
                            {
                                dataSetMessageLog.Log.AddLogRow(entry.Time.ToString(), LogEntry.GetLogType(entry.LogType),
                                                                entry.Message,entry.File,entry.Line,entry.Project);                                
                            }
                        }
                        else
                        {
                            dataSetMessageLog.Log.AddLogRow(entry.Time.ToString(), LogEntry.GetLogType(entry.LogType),
                                                            entry.Message, entry.File, entry.Line, entry.Project);
                        }
                    }
                }
            }
            finally
            {
                dataSetMessageLog.Log.EndLoadData();
                dataSetMessageLog.Log.AcceptChanges();
                SetSymbols();
                initializing = false;
            }            
        }


        ///<summary>
        /// Adds a LogEntry to the dataset
        ///</summary>
        ///<param name="entry"></param>
        public void AddMessageLogEntry(LogEntry entry)
        {
            var selectedLogType = (LogType)comboBoxLogType.SelectedIndex;
            if (selectedLogType == LogType.None || selectedLogType == entry.LogType)
            {
                string type = LogEntry.GetLogType(entry.LogType);
                dataSetMessageLog.Log.AddLogRow(entry.Time.ToString(), type, entry.Message, entry.File, entry.Line, entry.Project);
                SetSymbols();
            }

        }

        private void ToolStripButtonSaveClick(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog { DefaultExt = ".txt", Filter = @"Text" + @"(*.txt)|*.txt|" + @"All Files" + @"(*.*)|*.*" };
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                var stream = new FileStream(saveDialog.FileName, FileMode.Create);
                try
                {
                    foreach (DataRow dataRow in dataSetMessageLog.Log.Rows)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(String.Format(logEntryRow, dataRow[0], dataRow[1], dataRow[2]));
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
                finally
                {
                    stream.Close();
                }
            }
        }

        private void ToolStripButtonClearClick(object sender, EventArgs e)
        {
            ClearLog();
        }

        public void ClearLog()
        {
            messageLog.ClearHistory();
            dataSetMessageLog.Log.Clear();
            dataSetMessageLog.Log.AcceptChanges();
            SetSymbols();
        }

        private void SetSymbols()
        {
            toolStripButtonSave.Enabled = dataSetMessageLog.Log.Rows.Count > 0;
            toolStripButtonClear.Enabled = toolStripButtonSave.Enabled;
        }


        private void ComboBoxLogTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initializing)
            {
                LogType selected;
                if (comboBoxLogType.SelectedItem is LogType)
                    selected = (LogType) comboBoxLogType.SelectedItem;
                else
                    selected = (LogType) comboBoxLogType.SelectedIndex;
                Initialize(messageLog, selected);
            }
        }


        private void LogGridControlDoubleClick(object sender, EventArgs e)
        {
            string time = gridViewLogEntries.GetFocusedDataRowItemText(0);
            string type = gridViewLogEntries.GetFocusedDataRowItemText(1);
            string message = gridViewLogEntries.GetFocusedDataRowItemText(2);
            if (!String.IsNullOrEmpty(message))
            {
                string file = gridViewLogEntries.GetFocusedDataRowItemText(3);
                int line = Convert.ToInt32(gridViewLogEntries.GetFocusedDataRowItemText(4));
                string project = gridViewLogEntries.GetFocusedDataRowItemText(5);

                LogType logType = LogEntry.GetLogType(type);

                var selectedEntry = new LogEntry(Convert.ToDateTime(time), logType, message, file, line, project);
                CompleXException.ShowLogEntry(selectedEntry);
            }
            //Logging.ShowEntry(selectedEntry);            
        }

        private void CheckBoxTimeCheckedChanged(object sender, EventArgs e)
        {
            LogType selected;
            if (comboBoxLogType.SelectedItem is LogType)
                selected = (LogType)comboBoxLogType.SelectedItem;
            else
                selected = (LogType)comboBoxLogType.SelectedIndex;
            Initialize(messageLog, selected);
            Settings.Set(Const.SettingShowLogFromTodayOnly, checkBoxTime.Checked);
        }

        private void PopupOptionsClick(object sender, EventArgs e)
        {
            var settingsPage = ApplicationHost.Host.GetService<ISettingsPage>(page => page.PageID.Equals("MessageLogOptions"));
            popupContainerOptions.Controls.Clear();            
            if (settingsPage != null && settingsPage.Initialize())
            {
               settingsPage.OnActivated(true,false);
               popupContainerOptions.Controls.Add(settingsPage.Control);
            }
        }


        private void PopupOptionsClosed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            var settingsPage = ApplicationHost.Host.GetService<ISettingsPage>(page => page.PageID.Equals("MessageLogOptions"));
            if (settingsPage != null )
            {
                settingsPage.OnOk();
            }
        }
    }
}