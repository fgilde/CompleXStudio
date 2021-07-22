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

namespace CompleX_Types
{
    [Serializable]
    public enum Priority
    {
        High,
        Normal,
        Low
    }

    [Serializable]
    public class TaskListEntry : ICloneable, IEditableObject, INotifyPropertyChanging, INotifyPropertyChanged
    {
        private string description;
        private bool finished;
        private Priority priority;

        public Priority Priority
        {
            get { return priority; }
            set
            {
                OnPropertyChanging("Priority");
                priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public bool Finished
        {
            get { return finished; }
            set
            {
                OnPropertyChanging("Finished");
                finished = value;
                OnPropertyChanged("Finished");
            }
        }

        public string  Description
        {
            get { return description; }
            set
            {
                OnPropertyChanging("Description");
                description = value;
                OnPropertyChanged("Description"); 
            }
        }


        public void SetValues(TaskListEntry source)
        {
            Description = source.Description;
            Priority = source.Priority;
            Finished = source.Finished;
        }

        #region Implementation of IEditableObject

        private TaskListEntry clone;

        public void BeginEdit()
        {
            clone = Clone() as TaskListEntry;
        }

        public void EndEdit()
        {
            clone = null;
        }

        public void CancelEdit()
        {
            if (clone != null)
            {
                SetValues(clone);
                clone = null;
            }
        }

        #endregion

        #region Implementation of ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Implementation of INotifyPropertyChanging

        [field: NonSerialized]
        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        #endregion
    }
}
