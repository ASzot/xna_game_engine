namespace BaseLogic.Editor.Forms.Object_Editors
{
    partial class TriggerEditorForm
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
            this.triggerListBox = new System.Windows.Forms.ListBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // triggerListBox
            // 
            this.triggerListBox.FormattingEnabled = true;
            this.triggerListBox.ItemHeight = 20;
            this.triggerListBox.Location = new System.Drawing.Point(12, 12);
            this.triggerListBox.Name = "triggerListBox";
            this.triggerListBox.Size = new System.Drawing.Size(228, 244);
            this.triggerListBox.TabIndex = 0;
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(247, 221);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(112, 35);
            this.closeBtn.TabIndex = 15;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.addBtn.Location = new System.Drawing.Point(247, 58);
            this.addBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(112, 35);
            this.addBtn.TabIndex = 16;
            this.addBtn.Text = "Add New";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.editBtn.Location = new System.Drawing.Point(247, 103);
            this.editBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(112, 35);
            this.editBtn.TabIndex = 17;
            this.editBtn.Text = "Edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.deleteBtn.Location = new System.Drawing.Point(247, 148);
            this.deleteBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(112, 35);
            this.deleteBtn.TabIndex = 18;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // TriggerEditorForm
            // 
            this.AcceptButton = this.addBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(384, 283);
            this.ControlBox = false;
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.triggerListBox);
            this.Name = "TriggerEditorForm";
            this.Text = "Trigger Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox triggerListBox;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button deleteBtn;
    }
}