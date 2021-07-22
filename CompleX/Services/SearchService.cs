using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Dialogs;
using CompleX.Properties;
using CompleX_Library;
using GrepWrap;

namespace CompleX.Services
{
    public static class SearchService
    {
        public static void ShowSearchDialog()
        {
            var searchReplaceControl = new SearchReplaceControl();
            var dlg = BaseDialogHelper.CreateBaseDialog(searchReplaceControl);

            dlg.OnAccept = () => Search(searchReplaceControl);
            dlg.VisibleInTaskBar = true;
            //dlg.TopMost = true;
            dlg.StartPosition = FormStartPosition.CenterScreen;
                                      
            dlg.CheckBox.Checked = dlg.TopMost;
          
            dlg.CheckBox.Text = Resources.StayOnTop;
            dlg.IsValid = () => searchReplaceControl.SearchText == String.Empty ? new ValidationResult(false, Resources.SearchTextEmpty) : new ValidationResult(true);
            dlg.CheckBox.Visible = true;
            dlg.CheckBox.CheckedChanged += (sender, args) => dlg.TopMost = dlg.CheckBox.Checked;
            dlg.Text =Resources.Search;
            dlg.Show();
        }

        private static void Search(SearchReplaceControl searchReplaceControl)
        {
            IEnumerable<string> files = Enumerable.Empty<string>();
            IEnumerable<Occurence> res;
            // Im aktuellem dokument suchen
            if (searchReplaceControl.SearchLocation == SearchLocation.CurrentDocument && CompleX_Studio.CurrentContentEditor != null)
            {
                res = FindWrapper.FindInString(CompleX_Studio.CurrentContentEditor.Content.ToString(), searchReplaceControl.SearchText, searchReplaceControl.SearchOptions);
            }
            // in aktueller selektion suchen
            else if(searchReplaceControl.SearchLocation == SearchLocation.Selection && CompleX_Studio.CurrentContentEditor != null)
            {
                res = FindWrapper.FindInString(CompleX_Studio.CurrentContentEditor.SelectedContent.ToString(), searchReplaceControl.SearchText, searchReplaceControl.SearchOptions);
            }
            else
            {
                // in bestimmten dateien suchen
                if (searchReplaceControl.SearchLocation == SearchLocation.OpenFiles) // alle offenen datein
                    files = FileService.OpenFiles.Select(edit => edit.GetFileName()).Where(File.Exists);
                if (searchReplaceControl.SearchLocation == SearchLocation.CurrentProject && ProjectService.IsProjectOpen) // alle projektdateien
                    files = ProjectService.ProjectFiles;
                if (searchReplaceControl.SearchLocation == SearchLocation.OpenProjectFiles && ProjectService.IsProjectOpen) // offene projekt dateien
                    files = FileService.OpenFiles.Select(edit => edit.GetFileName()).Where(s => ProjectService.ProjectFiles.Contains(s));
                if (files.Count() > 0)
                {
                    res = FindWrapper.FindInFile(files, searchReplaceControl.SearchText,
                                                 searchReplaceControl.SearchOptions);
                }
                else
                {
                    MessageService.ShowInformation(Resources.SearchFilesEmpty, Resources.Search);
                    return;
                }
            }

            if (res != null && res.Count() > 0)
            {
                CompleX_Studio.Instance.findResultsControl.UpdateData(res);
                CompleX_Studio.Instance.GetTopDockPanel(CompleX_Studio.Instance.dockPanelFindResults).Show();
            }
            else
            {
                MessageService.ShowInformation(Resources.SearchResultEmpty, Resources.Search);

            }
        }
    }

    public class SearchLocation : IEquatable<SearchLocation>
    {
        private SearchLocation(){}

        public string Text { get; set; }
        public int Key { get; set; }
        public SearchLocation(int key, string text)
        {
            Key = key;
            Text = text;
        }

        public override string  ToString()
        {
            return Text;
        }

        public static SearchLocation CurrentProject{ get{return new SearchLocation(0,Resources.SL_CurrentProject);}}
        public static SearchLocation CurrentDocument{ get{return new SearchLocation(1,Resources.SL_CurrentDocument);}}
        public static SearchLocation OpenFiles{ get{return new SearchLocation(2,Resources.SL_AllOpenFiles);}}
        public static SearchLocation OpenProjectFiles{ get{return new SearchLocation(3,Resources.SL_OpenFIlesInProject);}}
        public static SearchLocation Selection { get { return new SearchLocation(4, Resources.SL_Selection); } }

        #region Equality

        public bool Equals(SearchLocation other)
        {
            return Key.Equals(other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SearchLocation)) return false;
            return Equals((SearchLocation) obj);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public static bool operator ==(SearchLocation left, SearchLocation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SearchLocation left, SearchLocation right)
        {
            return !Equals(left, right);
        }

        #endregion

    }

}
