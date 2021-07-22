using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace CompleX.Controls
{
    public partial class FavoritesControl : System.Windows.Forms.UserControl {
        public event EventHandler AddNewFavorite;
        public event EventHandler EditFavorite;
        public event EventHandler DeleteFavorite;
        public event EventHandler OpenFavorite;
		
        public FavoritesControl() {
            InitializeComponent();
            ItemsEnabled();
        }

        private void ItemsEnabled() {
            iOpen.Enabled = iDelete.Enabled = iEdit.Enabled = listBox1.SelectedIndex >= 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e) {
            ItemsEnabled();
        }

        public void DeleteItems() {
            listBox1.Items.Clear();
        }

        public void AddItem(BarItem item, bool init) {
            listBox1.Items.Add(item.Caption);
            if(!init) listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void iAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(AddNewFavorite != null) AddNewFavorite(this, EventArgs.Empty);
        }

        private void iEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            int i = listBox1.SelectedIndex;
            if(EditFavorite != null && listBox1.SelectedItem != null) 
                EditFavorite(listBox1.SelectedItem.ToString(), EventArgs.Empty);
            listBox1.SelectedIndex = i;	
        }

        private void iDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            int i = listBox1.SelectedIndex;
            if(DeleteFavorite != null && listBox1.SelectedItem != null) 
                DeleteFavorite(listBox1.SelectedItem.ToString(), EventArgs.Empty);
            try {
                listBox1.SelectedIndex = i;	
            } catch {}
            ItemsEnabled();
        }

        private void DoOpenFavorite() {
            if(OpenFavorite != null && listBox1.SelectedItem != null) 
                OpenFavorite(listBox1.SelectedItem.ToString(), EventArgs.Empty);
        }

        private void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DoOpenFavorite();
        }

        private void listBox1_DoubleClick(object sender, System.EventArgs e) {
            DoOpenFavorite();
        }
    }
}