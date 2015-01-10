namespace RenderingSystem.Graphics.Forms
{
	partial class BloomSettingsForm
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
			this.bloomThresholdTxtBox = new System.Windows.Forms.TextBox();
			this.blurAmountTxtBox = new System.Windows.Forms.TextBox();
			this.bloomIntensityTxtBox = new System.Windows.Forms.TextBox();
			this.baseIntensityTxtBox = new System.Windows.Forms.TextBox();
			this.bloomSaturationTxtBox = new System.Windows.Forms.TextBox();
			this.baseSaturationTxtBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.acceptBtn = new System.Windows.Forms.Button();
			this.applyBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// bloomThresholdTxtBox
			// 
			this.bloomThresholdTxtBox.Location = new System.Drawing.Point(111, 12);
			this.bloomThresholdTxtBox.Name = "bloomThresholdTxtBox";
			this.bloomThresholdTxtBox.Size = new System.Drawing.Size(100, 20);
			this.bloomThresholdTxtBox.TabIndex = 0;
			// 
			// blurAmountTxtBox
			// 
			this.blurAmountTxtBox.Location = new System.Drawing.Point(111, 39);
			this.blurAmountTxtBox.Name = "blurAmountTxtBox";
			this.blurAmountTxtBox.Size = new System.Drawing.Size(100, 20);
			this.blurAmountTxtBox.TabIndex = 1;
			// 
			// bloomIntensityTxtBox
			// 
			this.bloomIntensityTxtBox.Location = new System.Drawing.Point(111, 65);
			this.bloomIntensityTxtBox.Name = "bloomIntensityTxtBox";
			this.bloomIntensityTxtBox.Size = new System.Drawing.Size(100, 20);
			this.bloomIntensityTxtBox.TabIndex = 2;
			// 
			// baseIntensityTxtBox
			// 
			this.baseIntensityTxtBox.Location = new System.Drawing.Point(111, 91);
			this.baseIntensityTxtBox.Name = "baseIntensityTxtBox";
			this.baseIntensityTxtBox.Size = new System.Drawing.Size(100, 20);
			this.baseIntensityTxtBox.TabIndex = 3;
			// 
			// bloomSaturationTxtBox
			// 
			this.bloomSaturationTxtBox.Location = new System.Drawing.Point(111, 117);
			this.bloomSaturationTxtBox.Name = "bloomSaturationTxtBox";
			this.bloomSaturationTxtBox.Size = new System.Drawing.Size(100, 20);
			this.bloomSaturationTxtBox.TabIndex = 4;
			// 
			// baseSaturationTxtBox
			// 
			this.baseSaturationTxtBox.Location = new System.Drawing.Point(111, 143);
			this.baseSaturationTxtBox.Name = "baseSaturationTxtBox";
			this.baseSaturationTxtBox.Size = new System.Drawing.Size(100, 20);
			this.baseSaturationTxtBox.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Bloom Threshold:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(38, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Blur Amount:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Bloom Intensity:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(29, 94);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Base Intensity:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Bloom Saturation:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(20, 146);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(85, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Base Saturation:";
			// 
			// acceptBtn
			// 
			this.acceptBtn.Location = new System.Drawing.Point(120, 169);
			this.acceptBtn.Name = "acceptBtn";
			this.acceptBtn.Size = new System.Drawing.Size(81, 43);
			this.acceptBtn.TabIndex = 13;
			this.acceptBtn.Text = "Accept";
			this.acceptBtn.UseVisualStyleBackColor = true;
			this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
			// 
			// applyBtn
			// 
			this.applyBtn.Location = new System.Drawing.Point(120, 218);
			this.applyBtn.Name = "applyBtn";
			this.applyBtn.Size = new System.Drawing.Size(81, 43);
			this.applyBtn.TabIndex = 14;
			this.applyBtn.Text = "Apply";
			this.applyBtn.UseVisualStyleBackColor = true;
			this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.Location = new System.Drawing.Point(120, 267);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(81, 43);
			this.cancelBtn.TabIndex = 15;
			this.cancelBtn.Text = "Close";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// BloomSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(275, 329);
			this.ControlBox = false;
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.applyBtn);
			this.Controls.Add(this.acceptBtn);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.baseSaturationTxtBox);
			this.Controls.Add(this.bloomSaturationTxtBox);
			this.Controls.Add(this.baseIntensityTxtBox);
			this.Controls.Add(this.bloomIntensityTxtBox);
			this.Controls.Add(this.blurAmountTxtBox);
			this.Controls.Add(this.bloomThresholdTxtBox);
			this.Name = "BloomSettingsForm";
			this.Text = "BloomSettingsForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox bloomThresholdTxtBox;
		private System.Windows.Forms.TextBox blurAmountTxtBox;
		private System.Windows.Forms.TextBox bloomIntensityTxtBox;
		private System.Windows.Forms.TextBox baseIntensityTxtBox;
		private System.Windows.Forms.TextBox bloomSaturationTxtBox;
		private System.Windows.Forms.TextBox baseSaturationTxtBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button acceptBtn;
		private System.Windows.Forms.Button applyBtn;
		private System.Windows.Forms.Button cancelBtn;
	}
}