namespace BaseLogic.Editor.Forms
{
	partial class WaterObjEditorForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.waterColorATxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.waterColorBTxtBox = new System.Windows.Forms.TextBox();
            this.waterColorRTxtBox = new System.Windows.Forms.TextBox();
            this.waterColorGTxtBox = new System.Windows.Forms.TextBox();
            this.scaleXTxtBox = new System.Windows.Forms.TextBox();
            this.scaleZTxtBox = new System.Windows.Forms.TextBox();
            this.texScaleYTxtBox = new System.Windows.Forms.TextBox();
            this.texScaleXTxtBox = new System.Windows.Forms.TextBox();
            this.translationSpeedTxtBox = new System.Windows.Forms.TextBox();
            this.waterColorFactorTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.translationDirZTxtBox = new System.Windows.Forms.TextBox();
            this.translationDirXTxtBox = new System.Windows.Forms.TextBox();
            this.translationDirYTxtBox = new System.Windows.Forms.TextBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.waveLengthTxtBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.waveHeightTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.waterColorATxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.waterColorBTxtBox);
            this.groupBox1.Controls.Add(this.waterColorRTxtBox);
            this.groupBox1.Controls.Add(this.waterColorGTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(218, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Water Color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "A:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "B:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-103, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "B:";
            // 
            // waterColorATxtBox
            // 
            this.waterColorATxtBox.Location = new System.Drawing.Point(31, 97);
            this.waterColorATxtBox.Name = "waterColorATxtBox";
            this.waterColorATxtBox.Size = new System.Drawing.Size(100, 20);
            this.waterColorATxtBox.TabIndex = 3;
            this.waterColorATxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "G:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "R:";
            // 
            // waterColorBTxtBox
            // 
            this.waterColorBTxtBox.Location = new System.Drawing.Point(31, 71);
            this.waterColorBTxtBox.Name = "waterColorBTxtBox";
            this.waterColorBTxtBox.Size = new System.Drawing.Size(100, 20);
            this.waterColorBTxtBox.TabIndex = 4;
            this.waterColorBTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // waterColorRTxtBox
            // 
            this.waterColorRTxtBox.Location = new System.Drawing.Point(31, 19);
            this.waterColorRTxtBox.Name = "waterColorRTxtBox";
            this.waterColorRTxtBox.Size = new System.Drawing.Size(100, 20);
            this.waterColorRTxtBox.TabIndex = 2;
            this.waterColorRTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // waterColorGTxtBox
            // 
            this.waterColorGTxtBox.Location = new System.Drawing.Point(31, 45);
            this.waterColorGTxtBox.Name = "waterColorGTxtBox";
            this.waterColorGTxtBox.Size = new System.Drawing.Size(100, 20);
            this.waterColorGTxtBox.TabIndex = 1;
            this.waterColorGTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // scaleXTxtBox
            // 
            this.scaleXTxtBox.Location = new System.Drawing.Point(112, 116);
            this.scaleXTxtBox.Name = "scaleXTxtBox";
            this.scaleXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.scaleXTxtBox.TabIndex = 1;
            this.scaleXTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // scaleZTxtBox
            // 
            this.scaleZTxtBox.Location = new System.Drawing.Point(112, 142);
            this.scaleZTxtBox.Name = "scaleZTxtBox";
            this.scaleZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.scaleZTxtBox.TabIndex = 2;
            this.scaleZTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // texScaleYTxtBox
            // 
            this.texScaleYTxtBox.Location = new System.Drawing.Point(112, 90);
            this.texScaleYTxtBox.Name = "texScaleYTxtBox";
            this.texScaleYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.texScaleYTxtBox.TabIndex = 6;
            this.texScaleYTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // texScaleXTxtBox
            // 
            this.texScaleXTxtBox.Location = new System.Drawing.Point(112, 64);
            this.texScaleXTxtBox.Name = "texScaleXTxtBox";
            this.texScaleXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.texScaleXTxtBox.TabIndex = 7;
            this.texScaleXTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // translationSpeedTxtBox
            // 
            this.translationSpeedTxtBox.Location = new System.Drawing.Point(112, 38);
            this.translationSpeedTxtBox.Name = "translationSpeedTxtBox";
            this.translationSpeedTxtBox.Size = new System.Drawing.Size(100, 20);
            this.translationSpeedTxtBox.TabIndex = 8;
            this.translationSpeedTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // waterColorFactorTxtBox
            // 
            this.waterColorFactorTxtBox.Location = new System.Drawing.Point(112, 12);
            this.waterColorFactorTxtBox.Name = "waterColorFactorTxtBox";
            this.waterColorFactorTxtBox.Size = new System.Drawing.Size(100, 20);
            this.waterColorFactorTxtBox.TabIndex = 9;
            this.waterColorFactorTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Translation Speed:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Tex ScaleY:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Scale X:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Tex ScaleX:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(59, 145);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Scale Z:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Water Color Factor:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.translationDirZTxtBox);
            this.groupBox2.Controls.Add(this.translationDirXTxtBox);
            this.groupBox2.Controls.Add(this.translationDirYTxtBox);
            this.groupBox2.Location = new System.Drawing.Point(218, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 102);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Translation Direction";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 74);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Z:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(-103, 74);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "B:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "Y:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 13);
            this.label19.TabIndex = 8;
            this.label19.Text = "X:";
            // 
            // translationDirZTxtBox
            // 
            this.translationDirZTxtBox.Location = new System.Drawing.Point(31, 71);
            this.translationDirZTxtBox.Name = "translationDirZTxtBox";
            this.translationDirZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.translationDirZTxtBox.TabIndex = 4;
            this.translationDirZTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // translationDirXTxtBox
            // 
            this.translationDirXTxtBox.Location = new System.Drawing.Point(31, 19);
            this.translationDirXTxtBox.Name = "translationDirXTxtBox";
            this.translationDirXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.translationDirXTxtBox.TabIndex = 2;
            this.translationDirXTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // translationDirYTxtBox
            // 
            this.translationDirYTxtBox.Location = new System.Drawing.Point(31, 45);
            this.translationDirYTxtBox.Name = "translationDirYTxtBox";
            this.translationDirYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.translationDirYTxtBox.TabIndex = 1;
            this.translationDirYTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(285, 284);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(90, 36);
            this.closeBtn.TabIndex = 19;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Wave Length:";
            // 
            // waveLengthTxtBox
            // 
            this.waveLengthTxtBox.Location = new System.Drawing.Point(112, 168);
            this.waveLengthTxtBox.Name = "waveLengthTxtBox";
            this.waveLengthTxtBox.Size = new System.Drawing.Size(100, 20);
            this.waveLengthTxtBox.TabIndex = 20;
            this.waveLengthTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(33, 197);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Wave Height:";
            // 
            // waveHeightTxtBox
            // 
            this.waveHeightTxtBox.Location = new System.Drawing.Point(112, 194);
            this.waveHeightTxtBox.Name = "waveHeightTxtBox";
            this.waveHeightTxtBox.Size = new System.Drawing.Size(100, 20);
            this.waveHeightTxtBox.TabIndex = 22;
            this.waveHeightTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // WaterObjEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 332);
            this.ControlBox = false;
            this.Controls.Add(this.label11);
            this.Controls.Add(this.waveHeightTxtBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.waveLengthTxtBox);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.waterColorFactorTxtBox);
            this.Controls.Add(this.translationSpeedTxtBox);
            this.Controls.Add(this.texScaleXTxtBox);
            this.Controls.Add(this.texScaleYTxtBox);
            this.Controls.Add(this.scaleZTxtBox);
            this.Controls.Add(this.scaleXTxtBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "WaterObjEditorForm";
            this.Text = "Water Object Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox waterColorBTxtBox;
		private System.Windows.Forms.TextBox waterColorRTxtBox;
		private System.Windows.Forms.TextBox waterColorGTxtBox;
		private System.Windows.Forms.TextBox waterColorATxtBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox scaleXTxtBox;
        private System.Windows.Forms.TextBox scaleZTxtBox;
		private System.Windows.Forms.TextBox texScaleYTxtBox;
		private System.Windows.Forms.TextBox texScaleXTxtBox;
		private System.Windows.Forms.TextBox translationSpeedTxtBox;
		private System.Windows.Forms.TextBox waterColorFactorTxtBox;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox translationDirZTxtBox;
		private System.Windows.Forms.TextBox translationDirXTxtBox;
		private System.Windows.Forms.TextBox translationDirYTxtBox;
		private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox waveLengthTxtBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox waveHeightTxtBox;
	}
}