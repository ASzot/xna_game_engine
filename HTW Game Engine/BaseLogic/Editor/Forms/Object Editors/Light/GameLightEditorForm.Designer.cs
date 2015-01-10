namespace BaseLogic.Editor.Forms
{
	partial class GameLightEditorForm
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
            this.lightListBox = new System.Windows.Forms.ListBox();
            this.lightDataPanel = new System.Windows.Forms.Panel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.addLightBtn = new System.Windows.Forms.Button();
            this.deleteLightBtn = new System.Windows.Forms.Button();
            this.editModifiersBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lightListBox
            // 
            this.lightListBox.FormattingEnabled = true;
            this.lightListBox.Location = new System.Drawing.Point(12, 12);
            this.lightListBox.Name = "lightListBox";
            this.lightListBox.Size = new System.Drawing.Size(209, 251);
            this.lightListBox.TabIndex = 0;
            this.lightListBox.SelectedIndexChanged += new System.EventHandler(this.lightListBox_SelectedIndexChanged);
            this.lightListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lightListBox_KeyPress);
            // 
            // lightDataPanel
            // 
            this.lightDataPanel.Location = new System.Drawing.Point(227, 12);
            this.lightDataPanel.Name = "lightDataPanel";
            this.lightDataPanel.Size = new System.Drawing.Size(256, 606);
            this.lightDataPanel.TabIndex = 1;
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(9, 580);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(88, 40);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // addLightBtn
            // 
            this.addLightBtn.Location = new System.Drawing.Point(21, 268);
            this.addLightBtn.Name = "addLightBtn";
            this.addLightBtn.Size = new System.Drawing.Size(87, 40);
            this.addLightBtn.TabIndex = 3;
            this.addLightBtn.Text = "Add Light";
            this.addLightBtn.UseVisualStyleBackColor = true;
            this.addLightBtn.Click += new System.EventHandler(this.addLightBtn_Click);
            // 
            // deleteLightBtn
            // 
            this.deleteLightBtn.Location = new System.Drawing.Point(21, 314);
            this.deleteLightBtn.Name = "deleteLightBtn";
            this.deleteLightBtn.Size = new System.Drawing.Size(87, 40);
            this.deleteLightBtn.TabIndex = 4;
            this.deleteLightBtn.Text = "Delete Light";
            this.deleteLightBtn.UseVisualStyleBackColor = true;
            this.deleteLightBtn.Click += new System.EventHandler(this.deleteLightBtn_Click);
            // 
            // editModifiersBtn
            // 
            this.editModifiersBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.editModifiersBtn.Location = new System.Drawing.Point(113, 268);
            this.editModifiersBtn.Name = "editModifiersBtn";
            this.editModifiersBtn.Size = new System.Drawing.Size(88, 40);
            this.editModifiersBtn.TabIndex = 5;
            this.editModifiersBtn.Text = "Edit Modifiers";
            this.editModifiersBtn.UseVisualStyleBackColor = true;
            this.editModifiersBtn.Click += new System.EventHandler(this.editModifiersBtn_Click);
            // 
            // GameLightEditorForm
            // 
            this.AcceptButton = this.addLightBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(499, 630);
            this.ControlBox = false;
            this.Controls.Add(this.editModifiersBtn);
            this.Controls.Add(this.deleteLightBtn);
            this.Controls.Add(this.addLightBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.lightDataPanel);
            this.Controls.Add(this.lightListBox);
            this.Name = "GameLightEditorForm";
            this.Text = "Light Editor";
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lightListBox;
		private System.Windows.Forms.Panel lightDataPanel;
		private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button addLightBtn;
        private System.Windows.Forms.Button deleteLightBtn;
        private System.Windows.Forms.Button editModifiersBtn;
	}
}