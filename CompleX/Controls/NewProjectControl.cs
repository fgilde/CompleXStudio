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
using System.IO;
using System.Windows.Forms;
using CompleX.Helper;
using CompleX.Services;
using CompleX_Library;
using System.Linq;
using CompleX_Library.Helper;
using CompleX_Settings.Constants;
using Enumerable = System.Linq.Enumerable;


namespace CompleX.Controls
{
    public enum ProjectControlMode
    {
        FileNew,
        FileAdd,
        Project,
    }

    public partial class NewProjectControl : UserControl
    {

        private readonly List<string> history;

        /// <summary>
        /// ProjectControlMode
        /// </summary>
        public ProjectControlMode ControlMode;

        /// <summary>
        /// Gets or sets the selected template.
        /// </summary>
        /// <value>The selected template.</value>
        public ListViewItem SelectedTemplate { get; private set; }


        /// <summary>
        /// Gets or sets the project location.
        /// </summary>
        /// <value>The project location.</value>
        public string ProjectLocation
        {
            get { return comboBoxLocation.Text.AddDirectorySeparatorChar(); }
            set { comboBoxLocation.Text = value; }
        }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>The name of the item.</value>
        public string ItemName
        {
            get { return textEditName.Text; }
            set { textEditName.Text = value; }
        }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string ProjectName
        {
            get { return textEditProjectName.Text; }
            set { textEditProjectName.Text = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [create directory].
        /// </summary>
        /// <value><c>true</c> if [create directory]; otherwise, <c>false</c>.</value>
        public bool CreateDirectory
        {
            get { return checkCreateDir.Checked; }
            set { checkCreateDir.Checked = value; }
        }

        /// <summary>
        /// Gets the template file.
        /// </summary>
        /// <value>The template file.</value>
        public string TemplateFile
        {
            get
            {
                if (SelectedTemplate != null)
                    return SelectedTemplate.Name;
                return String.Empty;
            }
        }
        /// <summary>
        /// Gets the description file.
        /// </summary>
        /// <value>The description file.</value>
        public string DescriptionFile
        {
            get
            {
                return TemplateFile + ".description"; 
            }
        }

        /// <summary>
        /// Gets the project content file.
        /// </summary>
        /// <value>The project content file.</value>
        public string ProjectContentFile
        {
            get
            {
                return TemplateFile + ".content.zip";
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get
            {
                var streamReader = new StreamReader(DescriptionFile);
                string result = streamReader.ReadToEnd();
                streamReader.Close();
                return result; 
            }
        }


        /// <summary>
        /// Gibt an, ob das Control korrekt ausgefüllt wurde
        /// </summary>
        public ValidationResult IsValid()
        {
            if(SelectedTemplate == null && ControlMode != ProjectControlMode.FileNew)
               return new ValidationResult {ErrorMessage = Properties.Resources.EmptyItem, Result = false};
            if(comboBoxLocation.Visible && !Directory.Exists(comboBoxLocation.Text))
               return new ValidationResult {ErrorMessage = Properties.Resources.DirectoryNotExists, Result = false};
            if (String.IsNullOrEmpty(ItemName))
                return new ValidationResult { ErrorMessage = Properties.Resources.EmptyItemName, Result = false };

            if (String.IsNullOrEmpty(ProjectName) && ControlMode == ProjectControlMode.Project)
                return new ValidationResult { ErrorMessage = Properties.Resources.EmptyProjectName, Result = false };

            if (history != null && !history.Contains(comboBoxLocation.Text))
            {
                history.Add(comboBoxLocation.Text);
                CompleX_Settings.Settings.Set(@"History_ProjectLocation" + Name, history);
            }

            return new ValidationResult {ErrorMessage = String.Empty, Result = true};
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewProjectControl"/> class.
        /// </summary>
        public NewProjectControl():this(ProjectControlMode.Project)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="NewProjectControl"/> class.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public NewProjectControl(ProjectControlMode mode )
        {
            InitializeComponent();
            ControlMode = mode;
            if (mode == ProjectControlMode.FileAdd || mode == ProjectControlMode.FileNew)
            {
                Utilities.SetVisibility(false, labelLocation, textEditProjectName, comboBoxLocation, btnBrowse, labelProjectName,
                              checkCreateDir);

            }
            treeViewTypes.Nodes.InsertDirectories(Pathes.TemplatePath);
            CreateDirectory = true;

            if(comboBoxLocation.Visible)
            {
                history = CompleX_Settings.Settings.Get(@"History_ProjectLocation" + Name, Enumerable.Empty<string>().ToList());
                if (history.Count() > 0)
                    comboBoxLocation.Properties.Items.AddRange(history.Where(Directory.Exists).ToArray());
                if (String.IsNullOrEmpty(comboBoxLocation.Text))
                    comboBoxLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

        }


        private void TreeViewTypesAfterSelect(object sender, TreeViewEventArgs e)
        {
            string subpath = treeViewTypes.SelectedNode.AsString();

            if(ControlMode == ProjectControlMode.Project)
                listViewTemplates.AddFileItems(Pathes.TemplatePath.AddDirectorySeparatorChar() + subpath,".description",".zip");
            else
                listViewTemplates.AddFileItems(Pathes.TemplatePath.AddDirectorySeparatorChar() + subpath,s => !File.Exists(s+".content.zip"), ".description", ".zip");
            
            if (listViewTemplates.Items.Count <= 0)
                treeViewTypes.Nodes.Remove(treeViewTypes.SelectedNode);
            SetSelectedItem(null);     
        }

        private void ListViewTemplatesAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string fileNameOld = listViewTemplates.SelectedItems[0].Name;
            string fileNameNew = Path.GetDirectoryName(fileNameOld).AddDirectorySeparatorChar()+e.Label;
            e.CancelEdit = String.IsNullOrEmpty(e.Label);
            if(File.Exists(fileNameOld) && !e.CancelEdit && !String.IsNullOrEmpty(e.Label))
            {
                if (MessageService.AskDsaOnYes(String.Format(Properties.Resources.Confirm_Rename, Path.GetFileName(fileNameOld), Path.GetFileName(fileNameNew)), Text, @"RenameLabelEditNotification"))
                {
                    File.Move(fileNameOld, fileNameNew);
                    if(File.Exists(fileNameOld+".description"))
                        File.Move(fileNameOld + ".description", fileNameNew + ".description");
                    if (File.Exists(fileNameOld + ".content.zip"))
                        File.Move(fileNameOld + ".content.zip", fileNameNew + ".content.zip");

                }else
                {
                    e.CancelEdit = true;
                }
            }
        }

        private void ListViewTemplatesSelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedItem(listViewTemplates.FocusedItem);
        }

        private void SetSelectedItem(ListViewItem item)
        {
            SelectedTemplate = item;
            textEditDescription.Text = String.Empty;
            if (SelectedTemplate != null)
            {
                string name = SelectedTemplate.Name;
                string descriptionFile = name + ".description";
                if (File.Exists(descriptionFile))
                {
                    var streamReader = new StreamReader(descriptionFile);
                    textEditDescription.Text = streamReader.ReadToEnd();
                }
                textEditName.Text = SelectedTemplate.Text;
            }else
            {
                textEditName.Text = String.Empty;
            }
        }

        private void BtnBrowseClick(object sender, EventArgs e)
        {
            using (var selectDirDlg = new FolderBrowserDialog { SelectedPath = comboBoxLocation.Text })
            {
                if (selectDirDlg.ShowDialog() == DialogResult.OK)
                {
                    comboBoxLocation.Text = selectDirDlg.SelectedPath;
                    comboBoxLocation.Properties.Items.Add(comboBoxLocation.Text);
                }
            }
        }

        private void PictureBox1Click(object sender, EventArgs e)
        {
            listViewTemplates.View = View.LargeIcon;
        }

        private void PictureBox2Click(object sender, EventArgs e)
        {
            listViewTemplates.View = View.List;
        }

        private void TextEditNameEditValueChanged(object sender, EventArgs e)
        {
            ProjectName = Path.GetFileNameWithoutExtension(ItemName);
        }
    }
}
