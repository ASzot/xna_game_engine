namespace BaseLogic.Editor.Forms
{
	partial class CreatePointLightForm
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
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.posXTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.posYTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.posZTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.idTxtBox = new System.Windows.Forms.TextBox();
            this.intensityTxtBox = new System.Windows.Forms.TextBox();
            this.rangeTxtBox = new System.Windows.Forms.TextBox();
            this.colorComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.createBtn = new System.Windows.Forms.Button();
            this.specIntenTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(198, 163);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(90, 39);
            this.cancelBtn.TabIndex = 10;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(49, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 51;
            this.label15.Text = "Light ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.posXTxtBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.posYTxtBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.posZTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(225, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 100);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position:";
            // 
            // posXTxtBox
            // 
            this.posXTxtBox.Location = new System.Drawing.Point(31, 19);
            this.posXTxtBox.Name = "posXTxtBox";
            this.posXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posXTxtBox.TabIndex = 6;
            this.posXTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
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
            // posYTxtBox
            // 
            this.posYTxtBox.Location = new System.Drawing.Point(31, 45);
            this.posYTxtBox.Name = "posYTxtBox";
            this.posYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posYTxtBox.TabIndex = 7;
            this.posYTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
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
            // posZTxtBox
            // 
            this.posZTxtBox.Location = new System.Drawing.Point(31, 71);
            this.posZTxtBox.Name = "posZTxtBox";
            this.posZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posZTxtBox.TabIndex = 8;
            this.posZTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(62, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "Color:";
            // 
            // idTxtBox
            // 
            this.idTxtBox.Location = new System.Drawing.Point(102, 94);
            this.idTxtBox.Name = "idTxtBox";
            this.idTxtBox.Size = new System.Drawing.Size(100, 20);
            this.idTxtBox.TabIndex = 3;
            this.idTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // intensityTxtBox
            // 
            this.intensityTxtBox.Location = new System.Drawing.Point(102, 16);
            this.intensityTxtBox.Name = "intensityTxtBox";
            this.intensityTxtBox.Size = new System.Drawing.Size(100, 20);
            this.intensityTxtBox.TabIndex = 0;
            this.intensityTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // rangeTxtBox
            // 
            this.rangeTxtBox.Location = new System.Drawing.Point(102, 68);
            this.rangeTxtBox.Name = "rangeTxtBox";
            this.rangeTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rangeTxtBox.TabIndex = 2;
            this.rangeTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // colorComboBox
            // 
            this.colorComboBox.FormattingEnabled = true;
            this.colorComboBox.Location = new System.Drawing.Point(102, 120);
            this.colorComboBox.Name = "colorComboBox";
            this.colorComboBox.Size = new System.Drawing.Size(121, 21);
            this.colorComboBox.TabIndex = 4;
            this.colorComboBox.SelectedIndexChanged += new System.EventHandler(this.colorComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Range:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Intensity:";
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(102, 163);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(90, 39);
            this.createBtn.TabIndex = 9;
            this.createBtn.Text = "Create Light";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // specIntenTxtBox
            // 
            this.specIntenTxtBox.Location = new System.Drawing.Point(102, 42);
            this.specIntenTxtBox.Name = "specIntenTxtBox";
            this.specIntenTxtBox.Size = new System.Drawing.Size(100, 20);
            this.specIntenTxtBox.TabIndex = 1;
            this.specIntenTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Specular Intensity:";
            // 
            // CreatePointLightForm
            // 
            this.AcceptButton = this.createBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(377, 214);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.specIntenTxtBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.idTxtBox);
            this.Controls.Add(this.intensityTxtBox);
            this.Controls.Add(this.rangeTxtBox);
            this.Controls.Add(this.colorComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createBtn);
            this.Name = "CreatePointLightForm";
            this.Text = "Create Point Light";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox posXTxtBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox posYTxtBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox posZTxtBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox idTxtBox;
		private System.Windows.Forms.TextBox intensityTxtBox;
		private System.Windows.Forms.TextBox rangeTxtBox;
		private System.Windows.Forms.ComboBox colorComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.TextBox specIntenTxtBox;
        private System.Windows.Forms.Label label6;
	}
}