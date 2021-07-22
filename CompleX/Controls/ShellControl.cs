//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompleX.Helper;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library.Helper;
using CompleX_Settings;

namespace CompleX.Controls
{
    /// <summary>
    /// Summary description for ShellControl.
    /// </summary>
    public class ShellControl : UserControl
    {
        private ShellTextBox shellTextBox;
        public event EventCommandEntered CommandEntered;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private string directory = String.Empty;

        public string Dir
        {
            get
            {
                return directory;
            }
        }

        public ShellControl()
        {
            InitializeComponent();
            shellTextBox.ContextMenuStrip = PlaceHolder.GetPopUpMenu(s => shellTextBox.SelectedText = s, false);
            shellTextBox.KeyDown += ShellTextBoxOnKeyDown;
            directory = Path.GetDirectoryName(Application.ExecutablePath);
            if (UseAutoDirectoryPrompt)
                shellTextBox.Prompt = directory + ">";
        }


        private void ShellTextBoxOnKeyDown(object sender, KeyEventArgs args)
        {
            if(args.KeyCode == Keys.Tab)
            {
                args.Handled = true;
                string s = shellTextBox.Lines[shellTextBox.Lines.Count()-1].Substring(Prompt.Length);
                if(s.Contains(" "))
                {
                    int i = s.IndexOf(' ');
                    if(i >=0)
                        s = s.Substring(i+1);
                }
                var files = Directory.GetFiles(directory);
                string fileName = files.FirstOrDefault(s1 => Path.GetFileName(s1).StartsWith(s));
                if (!String.IsNullOrEmpty(fileName))
                {
                    fileName = Path.GetFileName(fileName);
                    shellTextBox.Text = shellTextBox.Text.Substring(0, shellTextBox.Text.Length - s.Length);
                    shellTextBox.Text += fileName;
                }
            }
        }

        internal void ExecuteCommand(string command)
        {
            command = PlaceHolder.ReplacePlaceHolder(command);
            if (!ProcessInternalCommand(command))
                DoDosCommand(command);
        }

        private void DoDosCommand(string command)
        {
            string output;
            string error;
            GetCmdResult(command, out output, out error);

            // p.WaitForExit();
            if (output.Length != 0)
                WriteText(output);
            else if (error.Length != 0)
                WriteText(error);

            GetCmdResult("cd", out output, out error);
            directory = output.Replace(Environment.NewLine, "");
            ChangeAutoDirPrompt();
        }

        private void ChangeAutoDirPrompt()
        {
            if (UseAutoDirectoryPrompt)
                shellTextBox.Prompt = directory + ">";
        }

        public void GetCmdResult(string command, out string output, out string error)
        {
            GetCommandResult(command, directory, false, out output, out error);
        }

        public static void GetCommandResult(string command,string directory, bool showConsole, out string output, out string error)
        {
            output = error = String.Empty;
            var startInfo = new ProcessStartInfo("cmd.exe")
                                {
                                    Arguments = "/C " + command,
                                    RedirectStandardError = true,
                                    RedirectStandardOutput = true,
                                    UseShellExecute = false,
                                    CreateNoWindow = !showConsole
                                };
            if (Directory.Exists(directory))
                startInfo.WorkingDirectory = directory;
            Process process = Process.Start(startInfo);
            if (process != null)
            {
                output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
            }          
        }

        private bool ProcessInternalCommand(string command)
        {
            command = command.ToLower();
            
            if (command.Length <= 3)
                command = CheckCommandIsDrive(command);
            
            switch (command)
            {
                case "cls":
                    Clear();
                    break;
                case "ftp":
                    Process.Start("ftp");
                    break;
                case "cmd":
                    Process.Start("cmd");
                    break;
                case "cmd help":
                    DoDosCommand("help");
                    break;
                case "clear history":
                    shellTextBox.ClearCommandHistory();
                    break;
                case "run history":
                        string[] cmds = GetCommandHistory();
                        foreach (string s in cmds)
                        {
                            if (s != command)
                                ExecuteCommand(s);
                        }
                    break;

                case "fg":
                    var dlg = new ColorDialog();
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        ShellTextForeColor = dlg.Color;
                        Settings.Set("ShellTextForeColor", dlg.Color);
                    }
                    break;
                case "bg":
                    var dlg2 = new ColorDialog();
                    if (dlg2.ShowDialog() == DialogResult.OK)
                    {
                        ShellTextBackColor = dlg2.Color;
                        Settings.Set("ShellTextBackColor", dlg2.Color);
                    }
                    break;
                case "delete settings":
                    if (MessageService.AskDsaOnYes(String.Format(Resources.DeleteFileConfirmation, Settings.SettingsFile), Resources.Delete, "DSA_DELSETTINGS_FROM_SHELLCONTROL"))
                       if(File.Exists(Settings.SettingsFile))
                           File.Delete(Settings.SettingsFile);
                    break;
                case "show settings":
                    WriteText(Settings.SettingsFile);
                    break;
                case "history":
                    {
                        string[] commands = GetCommandHistory();
                        var stringBuilder = new StringBuilder(commands.Length);
                        foreach (string s in commands)
                        {
                            stringBuilder.Append(s);
                            stringBuilder.Append(Environment.NewLine);
                        }
                        WriteText(stringBuilder.ToString());
                    }
                    break;
                case "?":
                case "help":
                    WriteText(GetHelpText());
                    break;
                default:
                    if (command.StartsWith("cd"))
                    {
                        if (!CheckChangeDirCommand(command)) return false;
                    }
                    else if (command.StartsWith("prompt"))
                    {
                        string[] parts = command.Split(new[] { '=' });
                        if (parts.Length == 2 && parts[0].Trim() == "prompt")
                            Prompt = parts[1].Trim();
                    }
                    else if (command.StartsWith("o "))
                    {
                        string fileName = command.Substring(2);
                        if (!File.Exists(fileName))
                            fileName = directory.AddDirectorySeparatorChar() + fileName;
                        if (File.Exists(fileName))
                        {
                            FileService.OpenFile(fileName);
                        }
                    }
                    else
                        return false;
                    break;
            }

            return true;
        }

        private static string CheckCommandIsDrive(string command)
        {
            var drives = DriveInfo.GetDrives();
            string result = command;
            command = command.ToUpper();
            if (command.Length == 1)
                command += ":";
            if (command.Length == 2)
                command += @"\";
            if (drives.Any(drive => command.Equals(drive.Name.ToUpper())))
            {
                return "cd " + command;
            }
            return result;
        }

        private bool CheckChangeDirCommand(string command)
        {
            if (command.Equals("cd..") || command.Equals("cd .."))
            {
                    var di = Directory.GetParent(directory);
                    if (di != null && di.Exists)
                    {
                        directory = di.FullName;
                        ChangeAutoDirPrompt();
                    }  
            }else
            {
                if (command.Length>3 && command.Substring(0,3) == "cd ")
                {
                    string newdirectory = command.Substring(3).Replace("\"",String.Empty);
                    if (Directory.Exists(newdirectory))
                    {
                        var di = new DirectoryInfo(newdirectory);
                        directory = di.FullName;
                        ChangeAutoDirPrompt();
                    }
                    else
                    {
                        string combinedPath = Path.Combine(directory, newdirectory);
                        if (Directory.Exists(combinedPath))
                        {
                            directory = combinedPath;
                            ChangeAutoDirPrompt();
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private static string GetHelpText()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("CompleX Command Window");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("*******************************************");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("Commands Available");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(1) All DOS commands that operate on a single line");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(2) prompt - Changes prompt. Usage (prompt=<desired_prompt>");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(3) history - prints history of entered commands");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(4) cls - Clears the screen");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(5) o {$FileName} - Opens the file in CompleX");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(6) fg - Opens a Dialog where you can change foreground color");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(7) bg - Opens a Dialog where you can change background color");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(8) run history - executes all commands in history again");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(9) clear history - clears commandhistory");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(10) cmd - DOS CMD window");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("(11) cmd help - show cmd help");
            stringBuilder.Append(Environment.NewLine);

            return stringBuilder.ToString();
        }

        internal void FireCommandEntered(string command)
        {
            OnCommandEntered(command);
        }

        protected virtual void OnCommandEntered(string command)
        {
            ExecuteCommand(command);
            if (CommandEntered != null)
            {
                CommandEntered(command, new CommandEnteredEventArgs(command));
            }
        }

        public Color ShellTextForeColor
        {
            get { return shellTextBox != null ? shellTextBox.ForeColor : Color.Green; }
            set 
            {
                if (shellTextBox != null)
                    shellTextBox.ForeColor = value;
            }
        }

        public Color ShellTextBackColor
        {
            get { return shellTextBox != null ? shellTextBox.BackColor:Color.Black; }
            set 
            { 
                if (shellTextBox != null)
                    shellTextBox.BackColor = value; 
            }
        }

        public Font ShellTextFont
        {
            get { return shellTextBox != null ? shellTextBox.Font: new Font("Tahoma", 8); }
            set 
            {
                if (shellTextBox != null)
                    shellTextBox.Font = value; 
            }
        }

        public void Clear()
        {
            shellTextBox.Clear();
        }

        public void WriteText(string text)
        {
            shellTextBox.WriteText(text);
        }

        public string[] GetCommandHistory()
        {
            return shellTextBox.GetCommandHistory();
        }

        /// <summary>
        /// Use auto change of prompt to dir
        /// </summary>
        public bool UseAutoDirectoryPrompt { get; set; }

        public string Prompt
        {
            get { return shellTextBox.Prompt; }
            set
            {
                    shellTextBox.Prompt = value;
            }
        }


        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.shellTextBox = new ShellTextBox();
            this.SuspendLayout();
            // 
            // shellTextBox
            // 
            this.shellTextBox.AcceptsReturn = true;
            this.shellTextBox.AcceptsTab = true;
            this.shellTextBox.BackColor = System.Drawing.Color.Black;
            this.shellTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellTextBox.ForeColor = System.Drawing.Color.LawnGreen;
            this.shellTextBox.Location = new System.Drawing.Point(0, 0);
            this.shellTextBox.Multiline = true;
            this.shellTextBox.Name = "shellTextBox";

            this.shellTextBox.Prompt = directory + ">";

            this.shellTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.shellTextBox.BackColor = System.Drawing.Color.Black;
            this.shellTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.shellTextBox.ForeColor = System.Drawing.Color.LawnGreen;
            this.shellTextBox.Size = new System.Drawing.Size(232, 216);
            this.shellTextBox.TabIndex = 0;
            this.shellTextBox.Text = "";
            // 
            // ShellControl
            // 
            this.Controls.Add(this.shellTextBox);
            this.Name = "ShellControl";
            this.Size = new System.Drawing.Size(232, 216);
            this.ResumeLayout(false);

        }
        #endregion
    }

    public class CommandEnteredEventArgs : EventArgs
    {
        readonly string command;
        public CommandEnteredEventArgs(string command)
        {
            this.command = command;
        }

        public string Command
        {
            get { return command; }
        }
    }

    public delegate void EventCommandEntered(object sender, CommandEnteredEventArgs e);
}