namespace BaseLogic.Editor.Forms
{
	partial class CreateSpotLightForm
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
            this.createBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.depthBiasTxtBox = new System.Windows.Forms.TextBox();
            this.querySizeTxtBox = new System.Windows.Forms.TextBox();
            this.colorComboBox = new System.Windows.Forms.ComboBox();
            this.spotExpTxtBox = new System.Windows.Forms.TextBox();
            this.spotAngleTxtBox = new System.Windows.Forms.TextBox();
            this.rangeTxtBox = new System.Windows.Forms.TextBox();
            this.intensityTxtBox = new System.Windows.Forms.TextBox();
            this.glowSizeTxtBox = new System.Windows.Forms.TextBox();
            this.idTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.posZTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.posYTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.posXTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.rotZTxtBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.rotYTxtBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.rotXTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.useLensFlareCheckBox = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.castShadowsCheckBox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.specIntenTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // createBtn
            // 
            this.createBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.createBtn.Location = new System.Drawing.Point(109, 298);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(90, 39);
            this.createBtn.TabIndex = 18;
            this.createBtn.Text = "Create Light";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Intensity:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Range:";
            // 
            // depthBiasTxtBox
            // 
            this.depthBiasTxtBox.Location = new System.Drawing.Point(99, 142);
            this.depthBiasTxtBox.Name = "depthBiasTxtBox";
            this.depthBiasTxtBox.Size = new System.Drawing.Size(100, 20);
            this.depthBiasTxtBox.TabIndex = 5;
            // 
            // querySizeTxtBox
            // 
            this.querySizeTxtBox.Location = new System.Drawing.Point(99, 169);
            this.querySizeTxtBox.Name = "querySizeTxtBox";
            this.querySizeTxtBox.Size = new System.Drawing.Size(100, 20);
            this.querySizeTxtBox.TabIndex = 6;
            // 
            // colorComboBox
            // 
            this.colorComboBox.FormattingEnabled = true;
            this.colorComboBox.Location = new System.Drawing.Point(99, 248);
            this.colorComboBox.Name = "colorComboBox";
            this.colorComboBox.Size = new System.Drawing.Size(121, 21);
            this.colorComboBox.TabIndex = 9;
            // 
            // spotExpTxtBox
            // 
            this.spotExpTxtBox.Location = new System.Drawing.Point(99, 116);
            this.spotExpTxtBox.Name = "spotExpTxtBox";
            this.spotExpTxtBox.Size = new System.Drawing.Size(100, 20);
            this.spotExpTxtBox.TabIndex = 4;
            // 
            // spotAngleTxtBox
            // 
            this.spotAngleTxtBox.Location = new System.Drawing.Point(99, 90);
            this.spotAngleTxtBox.Name = "spotAngleTxtBox";
            this.spotAngleTxtBox.Size = new System.Drawing.Size(100, 20);
            this.spotAngleTxtBox.TabIndex = 3;
            // 
            // rangeTxtBox
            // 
            this.rangeTxtBox.Location = new System.Drawing.Point(99, 64);
            this.rangeTxtBox.Name = "rangeTxtBox";
            this.rangeTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rangeTxtBox.TabIndex = 2;
            // 
            // intensityTxtBox
            // 
            this.intensityTxtBox.Location = new System.Drawing.Point(99, 12);
            this.intensityTxtBox.Name = "intensityTxtBox";
            this.intensityTxtBox.Size = new System.Drawing.Size(100, 20);
            this.intensityTxtBox.TabIndex = 0;
            // 
            // glowSizeTxtBox
            // 
            this.glowSizeTxtBox.Location = new System.Drawing.Point(99, 196);
            this.glowSizeTxtBox.Name = "glowSizeTxtBox";
            this.glowSizeTxtBox.Size = new System.Drawing.Size(100, 20);
            this.glowSizeTxtBox.TabIndex = 7;
            // 
            // idTxtBox
            // 
            this.idTxtBox.Location = new System.Drawing.Point(99, 222);
            this.idTxtBox.Name = "idTxtBox";
            this.idTxtBox.Size = new System.Drawing.Size(100, 20);
            this.idTxtBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Flare Glow Size:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Flare Query Size:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Depth Bias:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Spot Angle:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Spot Exponent:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(59, 251);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Color:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Z:";
            // 
            // posZTxtBox
            // 
            this.posZTxtBox.Location = new System.Drawing.Point(31, 71);
            this.posZTxtBox.Name = "posZTxtBox";
            this.posZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posZTxtBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Y:";
            // 
            // posYTxtBox
            // 
            this.posYTxtBox.Location = new System.Drawing.Point(31, 45);
            this.posYTxtBox.Name = "posYTxtBox";
            this.posYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posYTxtBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "X:";
            // 
            // posXTxtBox
            // 
            this.posXTxtBox.Location = new System.Drawing.Point(31, 19);
            this.posXTxtBox.Name = "posXTxtBox";
            this.posXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posXTxtBox.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.posXTxtBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.posYTxtBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.posZTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(239, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 100);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Z:";
            // 
            // rotZTxtBox
            // 
            this.rotZTxtBox.Location = new System.Drawing.Point(31, 71);
            this.rotZTxtBox.Name = "rotZTxtBox";
            this.rotZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotZTxtBox.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Y:";
            // 
            // rotYTxtBox
            // 
            this.rotYTxtBox.Location = new System.Drawing.Point(31, 45);
            this.rotYTxtBox.Name = "rotYTxtBox";
            this.rotYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotYTxtBox.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "X:";
            // 
            // rotXTxtBox
            // 
            this.rotXTxtBox.Location = new System.Drawing.Point(31, 19);
            this.rotXTxtBox.Name = "rotXTxtBox";
            this.rotXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotXTxtBox.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rotXTxtBox);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.rotYTxtBox);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.rotZTxtBox);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(239, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 100);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rotation";
            // 
            // useLensFlareCheckBox
            // 
            this.useLensFlareCheckBox.AutoSize = true;
            this.useLensFlareCheckBox.Location = new System.Drawing.Point(250, 228);
            this.useLensFlareCheckBox.Name = "useLensFlareCheckBox";
            this.useLensFlareCheckBox.Size = new System.Drawing.Size(97, 17);
            this.useLensFlareCheckBox.TabIndex = 16;
            this.useLensFlareCheckBox.Text = "Use Lens Flare";
            this.useLensFlareCheckBox.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(46, 225);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 27;
            this.label15.Text = "Light ID:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(205, 298);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(90, 39);
            this.cancelBtn.TabIndex = 19;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // castShadowsCheckBox
            // 
            this.castShadowsCheckBox.AutoSize = true;
            this.castShadowsCheckBox.Location = new System.Drawing.Point(250, 252);
            this.castShadowsCheckBox.Name = "castShadowsCheckBox";
            this.castShadowsCheckBox.Size = new System.Drawing.Size(94, 17);
            this.castShadowsCheckBox.TabIndex = 17;
            this.castShadowsCheckBox.Text = "Cast Shadows";
            this.castShadowsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(-1, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 13);
            this.label16.TabIndex = 28;
            this.label16.Text = "Specular Intensity:";
            // 
            // specIntenTxtBox
            // 
            this.specIntenTxtBox.Location = new System.Drawing.Point(99, 38);
            this.specIntenTxtBox.Name = "specIntenTxtBox";
            this.specIntenTxtBox.Size = new System.Drawing.Size(100, 20);
            this.specIntenTxtBox.TabIndex = 1;
            // 
            // CreateSpotLightForm
            // 
            this.AcceptButton = this.createBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(413, 365);
            this.Controls.Add(this.specIntenTxtBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.castShadowsCheckBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.useLensFlareCheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.idTxtBox);
            this.Controls.Add(this.glowSizeTxtBox);
            this.Controls.Add(this.intensityTxtBox);
            this.Controls.Add(this.rangeTxtBox);
            this.Controls.Add(this.spotAngleTxtBox);
            this.Controls.Add(this.spotExpTxtBox);
            this.Controls.Add(this.colorComboBox);
            this.Controls.Add(this.querySizeTxtBox);
            this.Controls.Add(this.depthBiasTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createBtn);
            this.Name = "CreateSpotLightForm";
            this.Text = "Create Spot Light";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button createBtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox depthBiasTxtBox;
		private System.Windows.Forms.TextBox querySizeTxtBox;
		private System.Windows.Forms.ComboBox colorComboBox;
		private System.Windows.Forms.TextBox spotExpTxtBox;
		private System.Windows.Forms.TextBox spotAngleTxtBox;
		private System.Windows.Forms.TextBox rangeTxtBox;
		private System.Windows.Forms.TextBox intensityTxtBox;
		private System.Windows.Forms.TextBox glowSizeTxtBox;
		private System.Windows.Forms.TextBox idTxtBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox posZTxtBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox posYTxtBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox posXTxtBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox rotZTxtBox;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox rotYTxtBox;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox rotXTxtBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox useLensFlareCheckBox;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.CheckBox castShadowsCheckBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox specIntenTxtBox;

	}
}