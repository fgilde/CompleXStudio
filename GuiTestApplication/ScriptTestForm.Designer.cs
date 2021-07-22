namespace GuiTestApplication
{
    partial class ScriptTestForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.TB_Script = new System.Windows.Forms.TextBox();
            this.TB_Info = new System.Windows.Forms.TextBox();
            this.BT_Execute = new System.Windows.Forms.Button();
            this.RB_Language_CS = new System.Windows.Forms.RadioButton();
            this.RB_Language_VB = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // TB_Script
            // 
            this.TB_Script.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                           | System.Windows.Forms.AnchorStyles.Left)
                                                                          | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Script.Location = new System.Drawing.Point(12, 12);
            this.TB_Script.Multiline = true;
            this.TB_Script.Name = "TB_Script";
            this.TB_Script.Size = new System.Drawing.Size(687, 345);
            this.TB_Script.TabIndex = 0;
            this.TB_Script.Text = "this.Context.WriteLine(this.Context.UserName);";
            // 
            // TB_Info
            // 
            this.TB_Info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                                                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Info.Location = new System.Drawing.Point(12, 392);
            this.TB_Info.Multiline = true;
            this.TB_Info.Name = "TB_Info";
            this.TB_Info.ReadOnly = true;
            this.TB_Info.Size = new System.Drawing.Size(687, 146);
            this.TB_Info.TabIndex = 1;
            // 
            // BT_Execute
            // 
            this.BT_Execute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BT_Execute.Location = new System.Drawing.Point(12, 363);
            this.BT_Execute.Name = "BT_Execute";
            this.BT_Execute.Size = new System.Drawing.Size(114, 23);
            this.BT_Execute.TabIndex = 2;
            this.BT_Execute.Text = "Execute Script";
            this.BT_Execute.UseVisualStyleBackColor = true;
            this.BT_Execute.Click += new System.EventHandler(this.BT_Execute_Click);
            // 
            // RB_Language_CS
            // 
            this.RB_Language_CS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RB_Language_CS.AutoSize = true;
            this.RB_Language_CS.Checked = true;
            this.RB_Language_CS.Location = new System.Drawing.Point(132, 366);
            this.RB_Language_CS.Name = "RB_Language_CS";
            this.RB_Language_CS.Size = new System.Drawing.Size(39, 17);
            this.RB_Language_CS.TabIndex = 3;
            this.RB_Language_CS.TabStop = true;
            this.RB_Language_CS.Text = "C#";
            this.RB_Language_CS.UseVisualStyleBackColor = true;
            // 
            // RB_Language_VB
            // 
            this.RB_Language_VB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RB_Language_VB.AutoSize = true;
            this.RB_Language_VB.Location = new System.Drawing.Point(177, 366);
            this.RB_Language_VB.Name = "RB_Language_VB";
            this.RB_Language_VB.Size = new System.Drawing.Size(67, 17);
            this.RB_Language_VB.TabIndex = 4;
            this.RB_Language_VB.Text = "VB .NET";
            this.RB_Language_VB.UseVisualStyleBackColor = true;
            // 
            // ScriptTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 550);
            this.Controls.Add(this.RB_Language_VB);
            this.Controls.Add(this.RB_Language_CS);
            this.Controls.Add(this.BT_Execute);
            this.Controls.Add(this.TB_Info);
            this.Controls.Add(this.TB_Script);
            this.Name = "ScriptTestForm";
            this.Text = "Scripting Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Script;
        private System.Windows.Forms.TextBox TB_Info;
        private System.Windows.Forms.Button BT_Execute;
        private System.Windows.Forms.RadioButton RB_Language_CS;
        private System.Windows.Forms.RadioButton RB_Language_VB;
    }
}