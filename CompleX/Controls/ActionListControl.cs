//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace CompleX.Controls
{

    /// <summary>
    /// ActionListControl
    /// </summary>
    public partial class ActionListControl : UserControl, IEnumerable<NamedAction>
    {
        private readonly List<NamedAction> actionList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionListControl"/> class.
        /// </summary>
        public ActionListControl()
        {
            actionList = new List<NamedAction>();
            InitializeComponent();
        }

        /// <summary>
        /// Gets the selected action.
        /// </summary>
        public NamedAction SelectedAction
        {
            get
            {
                if (actionlistBox.SelectedItem != null && actionlistBox.SelectedItem is NamedAction)
                    return (NamedAction)actionlistBox.SelectedItem;
                return null;
            }
        }

        /// <summary>
        /// Adds the specified action.
        /// </summary>
        public int Add(NamedAction action)
        {
            var tmpAction = actionList.FirstOrDefault(namedAction =>
                                               namedAction.Caption.Equals(action.Caption)
                                               && namedAction.Name.Equals(action.Name));
            if(tmpAction != null)
            {
                actionList.Remove(tmpAction); 
            }

            actionList.Add(action);
            RefreshView();
            return actionList.Count - 1;
        }

        /// <summary>
        /// Removes the specified action.
        /// </summary>
        public bool Remove(NamedAction action)
        {
            bool result = actionList.Remove(action );
            RefreshView();
            return result;
        }

        /// <summary>
        /// Clears all actions.
        /// </summary>
        public void Clear()
        {
            actionList.Clear();
            RefreshView();
        }

        #region Implementation of IEnumerable

        public IEnumerator<NamedAction> GetEnumerator()
        {
            return actionList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return actionList.GetEnumerator();
        }

        #endregion


        private void RefreshView()
        {
            actionlistBox.BeginUpdate();
            actionlistBox.Items.Clear();
            foreach (var namedAction in actionList.OrderByDescending(action => action.CreationTime))
            {
                actionlistBox.Items.Add(namedAction);
            }
            actionlistBox.EndUpdate();
        }

        private void actionlistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SelectedAction != null && SelectedAction.Action != null)
                SelectedAction.Action();
               
        }

    }


    /// <summary>
    /// NamedAction Class
    /// </summary>
    public class NamedAction
    {
        public NamedAction(string name, string caption, Action action)
        {
            if (caption.StartsWith("&"))
                caption = caption.Substring(1);
            Name = name;
            Action = action;
            Caption = caption;
            CreationTime = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Caption;
        }

    }

}
