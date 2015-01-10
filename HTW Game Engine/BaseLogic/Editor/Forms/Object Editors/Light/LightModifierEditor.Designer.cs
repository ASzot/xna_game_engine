namespace BaseLogic.Editor.Forms
{
    partial class LightModifierEditor
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
            this.modifierListBox = new System.Windows.Forms.ListBox();
            this.appliedToPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.appliedToListBox = new System.Windows.Forms.ListBox();
            this.addModifierBtn = new System.Windows.Forms.Button();
            this.deleteModifierBtn = new System.Windows.Forms.Button();
            this.editModifierBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.appliedToPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // modifierListBox
            // 
            this.modifierListBox.FormattingEnabled = true;
            this.modifierListBox.ItemHeight = 20;
            this.modifierListBox.Location = new System.Drawing.Point(13, 13);
            this.modifierListBox.Name = "modifierListBox";
            this.modifierListBox.Size = new System.Drawing.Size(234, 264);
            this.modifierListBox.TabIndex = 0;
            this.modifierListBox.SelectedIndexChanged += new System.EventHandler(this.modifierListBox_SelectedIndexChanged);
            // 
            // appliedToPanel
            // 
            this.appliedToPanel.Controls.Add(this.label1);
            this.appliedToPanel.Controls.Add(this.appliedToListBox);
            this.appliedToPanel.Location = new System.Drawing.Point(253, 12);
            this.appliedToPanel.Name = "appliedToPanel";
            this.appliedToPanel.Size = new System.Drawing.Size(231, 437);
            this.appliedToPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Applied To:";
            // 
            // appliedToListBox
            // 
            this.appliedToListBox.FormattingEnabled = true;
            this.appliedToListBox.ItemHeight = 20;
            this.appliedToListBox.Location = new System.Drawing.Point(0, 27);
            this.appliedToListBox.Name = "appliedToListBox";
            this.appliedToListBox.Size = new System.Drawing.Size(228, 404);
            this.appliedToListBox.TabIndex = 2;
            this.appliedToListBox.SelectedIndexChanged += new System.EventHandler(this.appliedToListBox_SelectedIndexChanged);
            // 
            // addModifierBtn
            // 
            this.addModifierBtn.Location = new System.Drawing.Point(13, 285);
            this.addModifierBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addModifierBtn.Name = "addModifierBtn";
            this.addModifierBtn.Size = new System.Drawing.Size(130, 62);
            this.addModifierBtn.TabIndex = 4;
            this.addModifierBtn.Text = "Add Modifier";
            this.addModifierBtn.UseVisualStyleBackColor = true;
            this.addModifierBtn.Click += new System.EventHandler(this.addModifierBtn_Click);
            // 
            // deleteModifierBtn
            // 
            this.deleteModifierBtn.Location = new System.Drawing.Point(13, 357);
            this.deleteModifierBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deleteModifierBtn.Name = "deleteModifierBtn";
            this.deleteModifierBtn.Size = new System.Drawing.Size(130, 62);
            this.deleteModifierBtn.TabIndex = 5;
            this.deleteModifierBtn.Text = "Delete Modifier";
            this.deleteModifierBtn.UseVisualStyleBackColor = true;
            this.deleteModifierBtn.Click += new System.EventHandler(this.deleteModifierBtn_Click);
            // 
            // editModifierBtn
            // 
            this.editModifierBtn.Location = new System.Drawing.Point(13, 429);
            this.editModifierBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.editModifierBtn.Name = "editModifierBtn";
            this.editModifierBtn.Size = new System.Drawing.Size(130, 62);
            this.editModifierBtn.TabIndex = 6;
            this.editModifierBtn.Text = "Edit Modifier";
            this.editModifierBtn.UseVisualStyleBackColor = true;
            this.editModifierBtn.Click += new System.EventHandler(this.editModifierBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(372, 457);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(112, 35);
            this.closeBtn.TabIndex = 17;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // LightModifierEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 506);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.editModifierBtn);
            this.Controls.Add(this.deleteModifierBtn);
            this.Controls.Add(this.addModifierBtn);
            this.Controls.Add(this.appliedToPanel);
            this.Controls.Add(this.modifierListBox);
            this.Name = "LightModifierEditor";
            this.Text = "LightModifierEditor";
            this.appliedToPanel.ResumeLayout(false);
            this.appliedToPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox modifierListBox;
        private System.Windows.Forms.Panel appliedToPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox appliedToListBox;
        private System.Windows.Forms.Button addModifierBtn;
        private System.Windows.Forms.Button deleteModifierBtn;
        private System.Windows.Forms.Button editModifierBtn;
        private System.Windows.Forms.Button closeBtn;
    }
}