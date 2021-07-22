//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CompleX.Services;
using GrepWrap;

namespace CompleX.Controls
{
    public partial class SearchReplaceControl : UserControl
    {
        public SearchReplaceControl()
        {
            InitializeComponent();
            GotFocus += (sender, args) => UpdateSearchLocations();
            UpdateSearchLocations();
            SetRegexTextBoxKind();
        }

        public SearchLocation SearchLocation { get; private set; }

        public string SearchText
        {
            get { return regexTextBox.Text; }
            set { regexTextBox.Text = value; }
        }

        public SearchOptions SearchOptions
        {
            get { return searchOptionsControl.SearchOptions; }
            set { searchOptionsControl.SearchOptions = value; }
        }

        private void UpdateSearchLocations()
        {
           comboBoxSearchLocation.Properties.Items.Clear();
           if (ProjectService.IsProjectOpen)
           {
               comboBoxSearchLocation.Properties.Items.Add(SearchLocation.CurrentProject);
               comboBoxSearchLocation.Properties.Items.Add(SearchLocation.OpenProjectFiles);
           }
            if(FileService.OpenFiles.Count()>0)
                comboBoxSearchLocation.Properties.Items.Add(SearchLocation.OpenFiles);
            comboBoxSearchLocation.Properties.Items.Add(SearchLocation.CurrentDocument);
            comboBoxSearchLocation.Properties.Items.Add(SearchLocation.Selection);
            comboBoxSearchLocation.SelectedIndex = 0;
        }

        private void SearchOptionsControlSearchOptionsChanged(object sender, EventArgs e)
        {
            SetRegexTextBoxKind();
        }

        private void SetRegexTextBoxKind()
        {
            if(0 != (searchOptionsControl.SearchOptions & SearchOptions.RegularExpression))
                regexTextBox.Kind = TextBoxKind.RegularExpression;
            else if(0 != (searchOptionsControl.SearchOptions & SearchOptions.WildCards))
                regexTextBox.Kind = TextBoxKind.Wildcard;
            else
                regexTextBox.Kind = TextBoxKind.None;
        }

        private void comboBoxSearchLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchLocation = (SearchLocation) comboBoxSearchLocation.SelectedItem;
        }
    }
}
