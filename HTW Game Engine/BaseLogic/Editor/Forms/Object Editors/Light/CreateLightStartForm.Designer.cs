namespace BaseLogic.Editor.Forms
{
	partial class CreateLightStartForm
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
            this.spotLightBtn = new System.Windows.Forms.Button();
            this.pointLightBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // spotLightBtn
            // 
            this.spotLightBtn.BackColor = System.Drawing.SystemColors.Control;
            this.spotLightBtn.Location = new System.Drawing.Point(210, 166);
            this.spotLightBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.spotLightBtn.Name = "spotLightBtn";
            this.spotLightBtn.Size = new System.Drawing.Size(132, 66);
            this.spotLightBtn.TabIndex = 0;
            this.spotLightBtn.Text = "Spot Light";
            this.spotLightBtn.UseVisualStyleBackColor = false;
            this.spotLightBtn.Click += new System.EventHandler(this.spotLightBtn_Click);
            // 
            // pointLightBtn
            // 
            this.pointLightBtn.BackColor = System.Drawing.SystemColors.Control;
            this.pointLightBtn.Location = new System.Drawing.Point(69, 166);
            this.pointLightBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pointLightBtn.Name = "pointLightBtn";
            this.pointLightBtn.Size = new System.Drawing.Size(132, 66);
            this.pointLightBtn.TabIndex = 1;
            this.pointLightBtn.Text = "Point Light";
            this.pointLightBtn.UseVisualStyleBackColor = false;
            this.pointLightBtn.Click += new System.EventHandler(this.pointLightBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label1.Location = new System.Drawing.Point(80, 112);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select the Type of Light to Create";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(142, 242);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 66);
            this.button1.TabIndex = 3;
            this.button1.Text = "Dir  Light";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.SystemColors.Control;
            this.closeBtn.Location = new System.Drawing.Point(142, 349);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(132, 48);
            this.closeBtn.TabIndex = 4;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // CreateLightStartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 403);
            this.ControlBox = false;
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pointLightBtn);
            this.Controls.Add(this.spotLightBtn);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CreateLightStartForm";
            this.Text = "Create Light";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button spotLightBtn;
		private System.Windows.Forms.Button pointLightBtn;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button closeBtn;
	}
}