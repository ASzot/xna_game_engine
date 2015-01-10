namespace RenderingSystem.Graphics.Forms
{
    partial class LightShaftsSettingsForm
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
            this.closeBtn = new System.Windows.Forms.Button();
            this.blendTxtBox = new System.Windows.Forms.TextBox();
            this.scaleTxtBox = new System.Windows.Forms.TextBox();
            this.spreadTxtBox = new System.Windows.Forms.TextBox();
            this.decayTxtBox = new System.Windows.Forms.TextBox();
            this.saturationTxtBox = new System.Windows.Forms.TextBox();
            this.contrastTxtBox = new System.Windows.Forms.TextBox();
            this.exposureTxtBox = new System.Windows.Forms.TextBox();
            this.shaftTintComboBox = new System.Windows.Forms.ComboBox();
            this.colorBalanceComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.rtSizeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(141, 289);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // blendTxtBox
            // 
            this.blendTxtBox.Location = new System.Drawing.Point(91, 12);
            this.blendTxtBox.Name = "blendTxtBox";
            this.blendTxtBox.Size = new System.Drawing.Size(100, 20);
            this.blendTxtBox.TabIndex = 1;
            this.blendTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // scaleTxtBox
            // 
            this.scaleTxtBox.Location = new System.Drawing.Point(91, 38);
            this.scaleTxtBox.Name = "scaleTxtBox";
            this.scaleTxtBox.Size = new System.Drawing.Size(100, 20);
            this.scaleTxtBox.TabIndex = 2;
            this.scaleTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // spreadTxtBox
            // 
            this.spreadTxtBox.Location = new System.Drawing.Point(91, 64);
            this.spreadTxtBox.Name = "spreadTxtBox";
            this.spreadTxtBox.Size = new System.Drawing.Size(100, 20);
            this.spreadTxtBox.TabIndex = 4;
            this.spreadTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // decayTxtBox
            // 
            this.decayTxtBox.Location = new System.Drawing.Point(91, 90);
            this.decayTxtBox.Name = "decayTxtBox";
            this.decayTxtBox.Size = new System.Drawing.Size(100, 20);
            this.decayTxtBox.TabIndex = 5;
            this.decayTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // saturationTxtBox
            // 
            this.saturationTxtBox.Location = new System.Drawing.Point(91, 116);
            this.saturationTxtBox.Name = "saturationTxtBox";
            this.saturationTxtBox.Size = new System.Drawing.Size(100, 20);
            this.saturationTxtBox.TabIndex = 6;
            this.saturationTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // contrastTxtBox
            // 
            this.contrastTxtBox.Location = new System.Drawing.Point(91, 142);
            this.contrastTxtBox.Name = "contrastTxtBox";
            this.contrastTxtBox.Size = new System.Drawing.Size(100, 20);
            this.contrastTxtBox.TabIndex = 7;
            this.contrastTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // exposureTxtBox
            // 
            this.exposureTxtBox.Location = new System.Drawing.Point(91, 168);
            this.exposureTxtBox.Name = "exposureTxtBox";
            this.exposureTxtBox.Size = new System.Drawing.Size(100, 20);
            this.exposureTxtBox.TabIndex = 8;
            this.exposureTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // shaftTintComboBox
            // 
            this.shaftTintComboBox.FormattingEnabled = true;
            this.shaftTintComboBox.Location = new System.Drawing.Point(91, 194);
            this.shaftTintComboBox.Name = "shaftTintComboBox";
            this.shaftTintComboBox.Size = new System.Drawing.Size(121, 21);
            this.shaftTintComboBox.TabIndex = 9;
            this.shaftTintComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectionChanged);
            // 
            // colorBalanceComboBox
            // 
            this.colorBalanceComboBox.FormattingEnabled = true;
            this.colorBalanceComboBox.Location = new System.Drawing.Point(91, 222);
            this.colorBalanceComboBox.Name = "colorBalanceComboBox";
            this.colorBalanceComboBox.Size = new System.Drawing.Size(121, 21);
            this.colorBalanceComboBox.TabIndex = 10;
            this.colorBalanceComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Blend:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Scale:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Spread:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Decay:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Shaft Tint:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Saturation:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Contrast:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Exposure:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Color Balance:";
            // 
            // rtSizeComboBox
            // 
            this.rtSizeComboBox.FormattingEnabled = true;
            this.rtSizeComboBox.Location = new System.Drawing.Point(91, 250);
            this.rtSizeComboBox.Name = "rtSizeComboBox";
            this.rtSizeComboBox.Size = new System.Drawing.Size(121, 21);
            this.rtSizeComboBox.TabIndex = 21;
            this.rtSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Buffer Size:";
            // 
            // LightShaftsSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 324);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtSizeComboBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorBalanceComboBox);
            this.Controls.Add(this.shaftTintComboBox);
            this.Controls.Add(this.exposureTxtBox);
            this.Controls.Add(this.contrastTxtBox);
            this.Controls.Add(this.saturationTxtBox);
            this.Controls.Add(this.decayTxtBox);
            this.Controls.Add(this.spreadTxtBox);
            this.Controls.Add(this.scaleTxtBox);
            this.Controls.Add(this.blendTxtBox);
            this.Controls.Add(this.closeBtn);
            this.Name = "LightShaftsSettingsForm";
            this.Text = "Light Shaft Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.TextBox blendTxtBox;
        private System.Windows.Forms.TextBox scaleTxtBox;
        private System.Windows.Forms.TextBox spreadTxtBox;
        private System.Windows.Forms.TextBox decayTxtBox;
        private System.Windows.Forms.TextBox saturationTxtBox;
        private System.Windows.Forms.TextBox contrastTxtBox;
        private System.Windows.Forms.TextBox exposureTxtBox;
        private System.Windows.Forms.ComboBox shaftTintComboBox;
        private System.Windows.Forms.ComboBox colorBalanceComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox rtSizeComboBox;
        private System.Windows.Forms.Label label3;
    }
}