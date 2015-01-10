namespace BaseLogic.Editor.Forms
{
    partial class SaveLevelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.acceptBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.filenameTxtBox = new System.Windows.Forms.TextBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.existingFilesListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // acceptBtn
            // 
            this.acceptBtn.Location = new System.Drawing.Point(81, 110);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(112, 40);
            this.acceptBtn.TabIndex = 0;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Save Filename:";
            // 
            // filenameTxtBox
            // 
            this.filenameTxtBox.Location = new System.Drawing.Point(119, 35);
            this.filenameTxtBox.Name = "filenameTxtBox";
            this.filenameTxtBox.Size = new System.Drawing.Size(100, 20);
            this.filenameTxtBox.TabIndex = 2;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(99, 156);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Existing Files:";
            // 
            // existingFilesListBox
            // 
            this.existingFilesListBox.FormattingEnabled = true;
            this.existingFilesListBox.Location = new System.Drawing.Point(249, 25);
            this.existingFilesListBox.Name = "existingFilesListBox";
            this.existingFilesListBox.Size = new System.Drawing.Size(120, 147);
            this.existingFilesListBox.TabIndex = 5;
            this.existingFilesListBox.SelectedIndexChanged += new System.EventHandler(this.existingFilesListBox_SelectedIndexChanged);
            // 
            // SaveLevelForm
            // 
            this.AcceptButton = this.acceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(407, 197);
            this.ControlBox = false;
            this.Controls.Add(this.existingFilesListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.filenameTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.acceptBtn);
            this.Name = "SaveLevelForm";
            this.Text = "Save Level";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filenameTxtBox;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox existingFilesListBox;
    }
}