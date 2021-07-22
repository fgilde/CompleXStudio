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
using System.Linq;
using System.Windows.Forms;
using CompleX.ServiceModel;
using CompleX_Library.Interfaces;

namespace CompleX.Controls
{

    public partial class SelectPluginControl<T> : UserControl where T : IHostedService
    {
        #region Delegating members

        public event EventHandler ItemActivate
        {
            add { listViewServices1.ItemActivate += value; }
            remove { listViewServices1.ItemActivate -= value; }
        }

        public event ItemCheckEventHandler ItemCheck
        {
            add { listViewServices1.ItemCheck += value; }
            remove { listViewServices1.ItemCheck -= value; }
        }

        public event ItemCheckedEventHandler ItemChecked
        {
            add { listViewServices1.ItemChecked += value; }
            remove { listViewServices1.ItemChecked -= value; }
        }

        public event ListViewItemSelectionChangedEventHandler ItemSelectionChanged
        {
            add { listViewServices1.ItemSelectionChanged += value; }
            remove { listViewServices1.ItemSelectionChanged -= value; }
        }

        public event ListViewItemMouseHoverEventHandler ItemMouseHover
        {
            add { listViewServices1.ItemMouseHover += value; }
            remove { listViewServices1.ItemMouseHover -= value; }
        }

        #endregion

        public SelectPluginControl():this(ApplicationHost.Host.GetServices<T>())
        {
        }

        public SelectPluginControl(IEnumerable<T> values)
        {
            InitializeComponent();
            Init(values);
        }

        public T SelectedService { get { return GetSelectedService(); } }

        public T GetSelectedService()
        {
            try
            {
                if (listViewServices1.SelectedItems[0] != null && listViewServices1.SelectedItems[0].Tag is T)
                    return (T)listViewServices1.SelectedItems[0].Tag;         
                return default(T);
            }
            catch 
            {
                return default(T);
            }
        }

        private void Init(IEnumerable<T> services)
        {
            listViewServices1.Items.Clear();
            
            if(services != null && services.Count() > 0)
            {
                foreach (T service in services)
                {
                    var item = new ListViewItem(service.ServiceName);
                    item.SubItems.Add(service.GetVersion().ToString());
                    item.SubItems.Add(service.GetType().ToString());
                    item.Tag = service;
                    listViewServices1.Items.Add(item);
                   
                }
            }
        }

    }

    //public class SelectPluginControl : SelectPluginControl<IHostedService>
    //{ }

}
