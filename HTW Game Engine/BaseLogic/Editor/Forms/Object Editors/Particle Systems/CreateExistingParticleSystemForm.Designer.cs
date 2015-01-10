namespace BaseLogic.Editor.Forms
{
	partial class CreateExistingParticleSystemForm
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
            this.particleSystemComboBox = new System.Windows.Forms.ComboBox();
            this.createBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.idTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.xPosTxtBox = new System.Windows.Forms.TextBox();
            this.zPosTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yPosTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // particleSystemComboBox
            // 
            this.particleSystemComboBox.FormattingEnabled = true;
            this.particleSystemComboBox.Location = new System.Drawing.Point(93, 144);
            this.particleSystemComboBox.Name = "particleSystemComboBox";
            this.particleSystemComboBox.Size = new System.Drawing.Size(121, 21);
            this.particleSystemComboBox.TabIndex = 0;
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(233, 12);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(75, 37);
            this.createBtn.TabIndex = 1;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID:";
            // 
            // idTxtBox
            // 
            this.idTxtBox.Location = new System.Drawing.Point(93, 12);
            this.idTxtBox.Name = "idTxtBox";
            this.idTxtBox.Size = new System.Drawing.Size(100, 20);
            this.idTxtBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Particle System:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(233, 55);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 37);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.xPosTxtBox);
            this.groupBox1.Controls.Add(this.zPosTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.yPosTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(60, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Z:";
            // 
            // xPosTxtBox
            // 
            this.xPosTxtBox.Location = new System.Drawing.Point(31, 19);
            this.xPosTxtBox.Name = "xPosTxtBox";
            this.xPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.xPosTxtBox.TabIndex = 7;
            // 
            // zPosTxtBox
            // 
            this.zPosTxtBox.Location = new System.Drawing.Point(31, 71);
            this.zPosTxtBox.Name = "zPosTxtBox";
            this.zPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.zPosTxtBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Y:";
            // 
            // yPosTxtBox
            // 
            this.yPosTxtBox.Location = new System.Drawing.Point(31, 45);
            this.yPosTxtBox.Name = "yPosTxtBox";
            this.yPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.yPosTxtBox.TabIndex = 9;
            // 
            // CreateExistingParticleSystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 218);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.idTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createBtn);
            this.Controls.Add(this.particleSystemComboBox);
            this.Name = "CreateExistingParticleSystemForm";
            this.Text = "Create Particle System";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox particleSystemComboBox;
		private System.Windows.Forms.Button createBtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox idTxtBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox xPosTxtBox;
		private System.Windows.Forms.TextBox zPosTxtBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox yPosTxtBox;
	}
}