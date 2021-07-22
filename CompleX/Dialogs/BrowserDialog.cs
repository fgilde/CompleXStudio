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
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CompleX_Library.Helper;
using CompleX_Settings.Constants;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace CompleX.Dialogs
{
    public partial class BrowserDialog : XtraForm
    {
        private string currentAddress = "";
        private readonly string linksName = Pathes.CommonApplicationData.AddDirectorySeparatorChar()+"br_links.xml";

        public BrowserDialog(string url)
        {
            InitializeComponent();
            webBrowser1.StatusTextChanged += WebBrowser1StatusTextChanged;
            webBrowser1.CanGoBackChanged += WebBrowser1CanGoBackChanged;
            webBrowser1.CanGoForwardChanged += WebBrowser1CanGoForwardChanged;
            webBrowser1.NewWindow += WebBrowser1OnNewWindow;
            barManager1.ForceLinkCreate();
            if (File.Exists(linksName))
            {
                var doc = new XmlDocument();
                try
                {
                    doc.Load(linksName);
                }
                catch (Exception e)
                {
                    CompleX_Studio.MessageLog.LogException(e);
                }
                if (doc.DocumentElement != null && doc.DocumentElement.Name == "Items")
                {
                    LoadLinks(doc.DocumentElement.ChildNodes[0].ChildNodes);
                    LoadFavorites(doc.DocumentElement.ChildNodes[1].ChildNodes);
                }
            }


            iFavorites.Down = dockPanel1.Visibility == DockVisibility.Visible;
            Focus();

            GoToItem(url);

        }

        private void WebBrowser1OnNewWindow(object sender, CancelEventArgs args)
        {
            if (sender is WebBrowser)
            {
                var dlg = new BrowserDialog(((WebBrowser)sender).StatusText);
                dlg.Show();
                args.Cancel = true;
            }
        }

        private string Address
        {
            get
            {
                if (barManager1.ActiveEditor != null && barManager1.ActiveEditor.EditValue != null)
                    return barManager1.ActiveEditor.EditValue.ToString();
                return null;
            }
        }

        private void LoadLinks(XmlNodeList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Item(i).Name == "Link")
                    AddNewItem(list[i].InnerText);
            }
        }

        private void LoadFavorites(XmlNodeList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Item(i).Name == "Favorite")
                    AddFavoriteItem(list[i].InnerText, list[i].Attributes[0].Value, true);
            }
            ChangeFavorites(true);

            ///

        }

        private void AddNewItem(string s)
        {
            if (s != "")
            {
                bool isAdded = false;
                for (int i = 0; i < repositoryItemComboBox1.Items.Count; i++)
                    if (repositoryItemComboBox1.Items[i].ToString() == s)
                    {
                        isAdded = true;
                        break;
                    }
                if (!isAdded)
                    repositoryItemComboBox1.Items.Add(s);
            }
        }

        private void GoToItem(string address)
        {
            if (address == null) return;
            if (currentAddress != address)
            {
                eAddress.EditValue = address;
                webBrowser1.Navigate(address);
            }
        }

        private void repositoryItemComboBox1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (barManager1.ActiveEditor != null)
                if (!((ComboBoxEdit) barManager1.ActiveEditor).IsPopupOpen &&
                    ((ComboBoxEdit) barManager1.ActiveEditor).SelectedIndex != -1)
                    GoToItem(Address);
        }

        private void repositoryItemComboBox1_CloseUp(object sender, CloseUpEventArgs e)
        {
            GoToItem(Address);
        }

        private void repositoryItemComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            var edit = sender as ComboBoxEdit;
            if (e.KeyData == Keys.Escape)
            {
                e.Handled = true;
                if (edit != null) edit.SelectAll();
            }
            if (e.KeyData == Keys.Enter)
            {
                barManager1.ActiveEditItemLink.PostEditor();
                if (edit != null) edit.SelectAll();
                e.Handled = true;
                GoToItem(eAddress.EditValue.ToString());
            }
        }

        private void SaveXml()
        {
            var tw = new XmlTextWriter(linksName, Encoding.UTF8) {Formatting = Formatting.Indented};
            tw.WriteStartElement("Items");
            tw.WriteAttributeString("version", "1.0");
            tw.WriteAttributeString("application", Application.ProductName);

            tw.WriteStartElement("Links");
            for (int i = 0; i < repositoryItemComboBox1.Items.Count; i++)
                tw.WriteElementString("Link", repositoryItemComboBox1.Items[i].ToString());
            tw.WriteEndElement();

            tw.WriteStartElement("Favorites");
            for (int i = 0; i < barManager1.Items.Count; i++)
                if (barManager1.Items[i].Category == barManager1.Categories["Favorites"])
                    tw.WriteElementString("Favorite", barManager1.Items[i].Tag.ToString(), barManager1.Items[i].Caption);
            tw.WriteEndElement();

            tw.WriteEndElement();
            tw.Close();

        }

        private void frmMain_Closing(object sender, CancelEventArgs e)
        {
           // SaveXml();
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            repositoryItemProgressBar1.Maximum =
                (int) (e.MaximumProgress + (e.MaximumProgress == repositoryItemProgressBar1.Minimum ? 1 : 0));
            eProgress.EditValue = e.CurrentProgress;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string s = e.Url.AbsoluteUri;
            if (barManager1.ActiveEditor != null)
                barManager1.ActiveEditItemLink.CloseEditor();
            if (CorrectAddress(s))
            {
                eAddress.EditValue = s;
                currentAddress = s;
                AddNewItem(s);
            }
        }

        private void WebBrowser1StatusTextChanged(object sender, EventArgs e)
        {
            iText.Caption = webBrowser1.StatusText;
        }

        private void WebBrowser1CanGoForwardChanged(object sender, EventArgs e)
        {
            iForward.Enabled = webBrowser1.CanGoForward;
        }

        private void WebBrowser1CanGoBackChanged(object sender, EventArgs e)
        {
            iBack.Enabled = webBrowser1.CanGoBack;
        }

        private static bool CorrectAddress(string name)
        {
            var names = new[] {"javascript:"};
            foreach (string s in names)
                if (name.IndexOf(s) == 0) return false;
            return true;
        }

        private void iGo_ItemClick(object sender, ItemClickEventArgs e)
        {
            GoToItem(eAddress.EditValue.ToString());
        }

        private void iBack_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                webBrowser1.GoBack();
            }
            catch (Exception ex)
            {
                CompleX_Studio.MessageLog.LogException(ex);
            }
        }

        private void iForward_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                webBrowser1.GoForward();
            }
            catch (Exception ex)
            {
                CompleX_Studio.MessageLog.LogException(ex);
            }
        }

        private void iStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            webBrowser1.Stop();
        }

        private void iRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void iHome_ItemClick(object sender, ItemClickEventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void iSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            webBrowser1.GoSearch();
        }


        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void iOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new OpenFileDialog
                          {
                              Filter = "HTML Files|*.htm; *.html|" +
                                       "GIF Files|*.gif|" +
                                       "JPEG Files|*.jpg;*.jpeg|" +
                                       "XML Files|*.xml|" +
                                       "All Files |*.*",
                              Title = "Open"
                          };
            if (dlg.ShowDialog() == DialogResult.OK)
                GoToItem(dlg.FileName);
        }

        private void iPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var pd = new PrintDocument();
                var dlg = new PrintDialog {Document = pd};
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pd.Print();
                }
            }
            catch (Exception ex)
            {
                CompleX_Studio.MessageLog.LogException(ex);
            }
        }

        private static void OpenNotepad()
        {
            var p = new Process();
            string s = Environment.SystemDirectory + "\\Notepad.exe";
            if (File.Exists(s))
            {
                p.StartInfo.FileName = s;
                p.Start();
            }
        }

        private void iEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenNotepad();
        }

        private void iFavorites_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (iFavorites.Down)
                dockPanel1.Visibility = DockVisibility.Visible;
            else
            {
                dockPanel1.Visibility = dockPanel1.Dock == DockingStyle.Float ? DockVisibility.Hidden : DockVisibility.AutoHide;
            }
        }

        private void AddFavoriteItem(string locationName, string locationUrl)
        {
            AddFavoriteItem(locationName, locationUrl, false);
        }

        private void AddFavoriteItem(string locationName, string locationUrl, bool init)
        {
            BarItem item = new BarButtonItem();
            item.ItemClick += Favorite_Click;
            item.Category = barManager1.Categories["Favorites"];
            item.Caption = locationName;
            item.Tag = locationUrl;
            barManager1.Items.Add(item);
            if (!init) ChangeFavorites();
        }

        private void AddFavorite()
        {
            var f = new AddFavoritesDialog(webBrowser1.DocumentTitle, webBrowser1.Url.AbsoluteUri, imageList1.Images[2]);
            if (f.ShowDialog() == DialogResult.OK)
            {
                bool add = true;
                for (int i = 0; i < barManager1.Items.Count; i++)
                {
                    BarItem item = barManager1.Items[i];
                    if (item.Category == barManager1.Categories["Favorites"] && item.Caption == f.LocationName)
                    {
                        if (
                            XtraMessageBox.Show(
                                "The name specified for the shortcut already exists in your Favorites list. Would you like to overwrite it?",
                                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            item.Tag = f.LocationURL;
                        add = false;
                        break;
                    }
                }
                if (add)
                    AddFavoriteItem(f.LocationName, f.LocationURL);
            }
        }

        private void iAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddFavorite();
        }

        private void Favorite_Click(object sender, ItemClickEventArgs e)
        {
            GoToItem(e.Item.Tag.ToString());
        }

        private void ChangeFavorites()
        {
            ChangeFavorites(false);
        }

        private void ChangeFavorites(bool init)
        {
            favorites.DeleteItems();

            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.Category == barManager1.Categories["Favorites"])
                {
                    favorites.AddItem(item, init);
                }
            }
        }

        private void ctrlFavorites1_AddNewFavorite(object sender, EventArgs e)
        {
            AddFavorite();
        }

        private BarItem ItemByName(string name)
        {
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.Category == barManager1.Categories["Favorites"] && item.Caption == name)
                    return item;
            }
            return null;
        }

        private void ctrlFavorites1_DeleteFavorite(object sender, EventArgs e)
        {
            string s = sender.ToString();
            if (
                XtraMessageBox.Show("Are you sure you want to remove shortcut?", Text, MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BarItem item = ItemByName(s);
                if (item != null)
                {
                    barManager1.Items.Remove(item);
                    ChangeFavorites();
                }
            }
        }

        private void ctrlFavorites1_EditFavorite(object sender, EventArgs e)
        {
            string s = sender.ToString();
            BarItem item = ItemByName(s);
            if (item != null)
            {
                var f = new AddFavoritesDialog(item.Caption, item.Tag.ToString(), imageList1.Images[1], false);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    item.Caption = f.LocationName;
                    item.Tag = f.LocationURL;
                    ChangeFavorites();
                }
            }
        }

        private void ctrlFavorites1_OpenFavorite(object sender, EventArgs e)
        {
            BarItem item = ItemByName(sender.ToString());
            if (item != null)
            {
                Favorite_Click(item, new ItemClickEventArgs(item, null));
            }
        }
    }
}