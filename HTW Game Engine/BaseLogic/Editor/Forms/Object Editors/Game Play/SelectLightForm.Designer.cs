namespace BaseLogic.Editor.Forms.Object_Editors
{
    partial class SelectLightForm
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
            this.selectLightListBox = new System.Windows.Forms.ListBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectLightListBox
            // 
            this.selectLightListBox.FormattingEnabled = true;
            this.selectLightListBox.ItemHeight = 20;
            this.selectLightListBox.Location = new System.Drawing.Point(12, 12);
            this.selectLightListBox.Name = "selectLightListBox";
            this.selectLightListBox.Size = new System.Drawing.Size(274, 304);
            this.selectLightListBox.TabIndex = 0;
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(173, 338);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(112, 35);
            this.closeBtn.TabIndex = 15;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // acceptBtn
            // 
            this.acceptBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptBtn.Location = new System.Drawing.Point(13, 338);
            this.acceptBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(112, 35);
            this.acceptBtn.TabIndex = 16;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // SelectLightForm
            // 
            this.AcceptButton = this.acceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(298, 387);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.selectLightListBox);
            this.Name = "SelectLightForm";
            this.Text = "SelectLightForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox selectLightListBox;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button acceptBtn;
    }
}