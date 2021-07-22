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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompleX.Properties;
using CompleX_Library.Interfaces;
using CompleX_Library;

namespace CompleX.Controls
{
    public enum MessageIconType
    {
        Error,
        Warning,
        Information,
        Question,
    }

    public partial class EditExceptionControl : BaseEditControl
    {
        private StreamReader streamReader;

        public EditExceptionControl(string message, MessageIconType icon,bool loadContent)
        {
            InitializeComponent();
            textLabel.Text = message;
           
            contentEdit.Properties.ReadOnly = true;

            LoadImage(icon);
            if (!contentEdit.Visible)
                this.Height -= contentEdit.Height;
            
            if(loadContent)
                contentEdit.Properties.ReadOnly = false;
        }

        public EditExceptionControl(Exception exception, MessageIconType icon,bool loadContent):this(exception.Message,icon,loadContent)
        {
            contentEdit.Text += Environment.NewLine+exception.StackTrace;
            if (icon != MessageIconType.Information && icon != MessageIconType.Question)
            {
                contentEdit.Visible = true;
                this.Height = 215;
            }
        }

        public EditExceptionControl(Exception exception, bool loadContent)
            : this(exception, MessageIconType.Error, loadContent)
        {
        }

        private void LoadImage(MessageIconType icon)
        {
            if (icon == MessageIconType.Error)
                ErrorImage.Image = Resources.error;
            if (icon == MessageIconType.Warning)
                ErrorImage.Image = Resources.Warning;
            if (icon == MessageIconType.Information)
                ErrorImage.Image = Resources.information;
            if (icon == MessageIconType.Question)
                ErrorImage.Image = Resources.questionmark;
        }

        public override Guid ID
        {
            get { return new Guid("E4241AED-CA6D-4C02-8487-B3E23648C48C"); }
        }

        public override object Content
        {
            get
            {
                return contentEdit.Text;
            }
            set
            {
                throw new NotImplementedException();     
            }
        }

        public override bool LoadFromFile(string fileName)
        {
            try
            {
                streamReader = new StreamReader(fileName);
                contentEdit.Text = streamReader.ReadToEnd();
                streamReader.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool SaveToFile(string fileName)
        {
            var writer = new StreamWriter(new FileStream(fileName,FileMode.Create));
            writer.Write(contentEdit.Text);
            writer.Close();
            return File.Exists(fileName);
        }

        public override void Insert(object obj)
        {
            contentEdit.Text.Insert(0, obj.ToString());
        }

        public override bool ContextMenuIsHandled
        {
            get { return true; }
        }   
    }
}
