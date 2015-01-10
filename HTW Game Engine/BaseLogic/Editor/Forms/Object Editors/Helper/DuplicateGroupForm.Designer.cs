namespace BaseLogic.Editor.Forms
{
    partial class DuplicateGroupForm
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
			this.selectedObjsListBox = new System.Windows.Forms.ListBox();
			this.groupOffsetGroupBox = new System.Windows.Forms.GroupBox();
			this.zOffsetTxtBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.yOffsetTxtBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.xOffsetTxtBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.closeBtn = new System.Windows.Forms.Button();
			this.duplicateBtn = new System.Windows.Forms.Button();
			this.groupOffsetGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// selectedObjsListBox
			// 
			this.selectedObjsListBox.FormattingEnabled = true;
			this.selectedObjsListBox.Location = new System.Drawing.Point(135, 8);
			this.selectedObjsListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.selectedObjsListBox.Name = "selectedObjsListBox";
			this.selectedObjsListBox.Size = new System.Drawing.Size(158, 173);
			this.selectedObjsListBox.TabIndex = 0;
			// 
			// groupOffsetGroupBox
			// 
			this.groupOffsetGroupBox.Controls.Add(this.zOffsetTxtBox);
			this.groupOffsetGroupBox.Controls.Add(this.label3);
			this.groupOffsetGroupBox.Controls.Add(this.yOffsetTxtBox);
			this.groupOffsetGroupBox.Controls.Add(this.label2);
			this.groupOffsetGroupBox.Controls.Add(this.xOffsetTxtBox);
			this.groupOffsetGroupBox.Controls.Add(this.label1);
			this.groupOffsetGroupBox.Location = new System.Drawing.Point(9, 8);
			this.groupOffsetGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupOffsetGroupBox.Name = "groupOffsetGroupBox";
			this.groupOffsetGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupOffsetGroupBox.Size = new System.Drawing.Size(122, 84);
			this.groupOffsetGroupBox.TabIndex = 1;
			this.groupOffsetGroupBox.TabStop = false;
			this.groupOffsetGroupBox.Text = "Group Offset";
			// 
			// zOffsetTxtBox
			// 
			this.zOffsetTxtBox.Location = new System.Drawing.Point(25, 58);
			this.zOffsetTxtBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.zOffsetTxtBox.Name = "zOffsetTxtBox";
			this.zOffsetTxtBox.Size = new System.Drawing.Size(85, 20);
			this.zOffsetTxtBox.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 60);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Z:";
			// 
			// yOffsetTxtBox
			// 
			this.yOffsetTxtBox.Location = new System.Drawing.Point(25, 37);
			this.yOffsetTxtBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.yOffsetTxtBox.Name = "yOffsetTxtBox";
			this.yOffsetTxtBox.Size = new System.Drawing.Size(85, 20);
			this.yOffsetTxtBox.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 39);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Y:";
			// 
			// xOffsetTxtBox
			// 
			this.xOffsetTxtBox.Location = new System.Drawing.Point(25, 16);
			this.xOffsetTxtBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.xOffsetTxtBox.Name = "xOffsetTxtBox";
			this.xOffsetTxtBox.Size = new System.Drawing.Size(85, 20);
			this.xOffsetTxtBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 18);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "X:";
			// 
			// closeBtn
			// 
			this.closeBtn.Location = new System.Drawing.Point(234, 187);
			this.closeBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(61, 25);
			this.closeBtn.TabIndex = 0;
			this.closeBtn.Text = "Close";
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// duplicateBtn
			// 
			this.duplicateBtn.Location = new System.Drawing.Point(8, 173);
			this.duplicateBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.duplicateBtn.Name = "duplicateBtn";
			this.duplicateBtn.Size = new System.Drawing.Size(86, 40);
			this.duplicateBtn.TabIndex = 2;
			this.duplicateBtn.Text = "Duplicate Group";
			this.duplicateBtn.UseVisualStyleBackColor = true;
			this.duplicateBtn.Click += new System.EventHandler(this.duplicateBtn_Click);
			// 
			// DuplicateGroupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(303, 220);
			this.Controls.Add(this.duplicateBtn);
			this.Controls.Add(this.closeBtn);
			this.Controls.Add(this.groupOffsetGroupBox);
			this.Controls.Add(this.selectedObjsListBox);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "DuplicateGroupForm";
			this.Text = "DuplicateGroupForm";
			this.groupOffsetGroupBox.ResumeLayout(false);
			this.groupOffsetGroupBox.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox selectedObjsListBox;
        private System.Windows.Forms.GroupBox groupOffsetGroupBox;
        private System.Windows.Forms.TextBox zOffsetTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox yOffsetTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xOffsetTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button duplicateBtn;
    }
}