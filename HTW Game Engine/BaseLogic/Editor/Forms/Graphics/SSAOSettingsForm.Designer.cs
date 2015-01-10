namespace BaseLogic.Editor.Forms
{
    partial class SSAOSettingsForm
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
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.randomTileTxtBox = new System.Windows.Forms.TextBox();
            this.radiusTxtBox = new System.Windows.Forms.TextBox();
            this.maxRadiusTxtBox = new System.Windows.Forms.TextBox();
            this.intensityTxtBox = new System.Windows.Forms.TextBox();
            this.blurCountTxtBox = new System.Windows.Forms.TextBox();
            this.biasTxtBox = new System.Windows.Forms.TextBox();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Location = new System.Drawing.Point(12, 12);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.Size = new System.Drawing.Size(65, 17);
            this.enabledCheckBox.TabIndex = 0;
            this.enabledCheckBox.Text = "Enabled";
            this.enabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // randomTileTxtBox
            // 
            this.randomTileTxtBox.Location = new System.Drawing.Point(77, 3);
            this.randomTileTxtBox.Name = "randomTileTxtBox";
            this.randomTileTxtBox.Size = new System.Drawing.Size(100, 20);
            this.randomTileTxtBox.TabIndex = 1;
            // 
            // radiusTxtBox
            // 
            this.radiusTxtBox.Location = new System.Drawing.Point(77, 29);
            this.radiusTxtBox.Name = "radiusTxtBox";
            this.radiusTxtBox.Size = new System.Drawing.Size(100, 20);
            this.radiusTxtBox.TabIndex = 2;
            // 
            // maxRadiusTxtBox
            // 
            this.maxRadiusTxtBox.Location = new System.Drawing.Point(77, 55);
            this.maxRadiusTxtBox.Name = "maxRadiusTxtBox";
            this.maxRadiusTxtBox.Size = new System.Drawing.Size(100, 20);
            this.maxRadiusTxtBox.TabIndex = 3;
            // 
            // intensityTxtBox
            // 
            this.intensityTxtBox.Location = new System.Drawing.Point(77, 81);
            this.intensityTxtBox.Name = "intensityTxtBox";
            this.intensityTxtBox.Size = new System.Drawing.Size(100, 20);
            this.intensityTxtBox.TabIndex = 4;
            // 
            // blurCountTxtBox
            // 
            this.blurCountTxtBox.Location = new System.Drawing.Point(77, 107);
            this.blurCountTxtBox.Name = "blurCountTxtBox";
            this.blurCountTxtBox.Size = new System.Drawing.Size(100, 20);
            this.blurCountTxtBox.TabIndex = 5;
            // 
            // biasTxtBox
            // 
            this.biasTxtBox.Location = new System.Drawing.Point(77, 133);
            this.biasTxtBox.Name = "biasTxtBox";
            this.biasTxtBox.Size = new System.Drawing.Size(100, 20);
            this.biasTxtBox.TabIndex = 6;
            // 
            // acceptBtn
            // 
            this.acceptBtn.Location = new System.Drawing.Point(56, 203);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(93, 39);
            this.acceptBtn.TabIndex = 7;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Random Tile:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Radius:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max Radius:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Intensity:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Blur Count:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Bias:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.randomTileTxtBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.radiusTxtBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.maxRadiusTxtBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.intensityTxtBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.blurCountTxtBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.biasTxtBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 162);
            this.panel1.TabIndex = 14;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(56, 279);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(93, 23);
            this.closeBtn.TabIndex = 15;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(56, 248);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(93, 23);
            this.applyBtn.TabIndex = 16;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // SSAOSettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(220, 314);
            this.ControlBox = false;
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.enabledCheckBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SSAOSettingsForm";
            this.Text = "SSAO Options";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.TextBox randomTileTxtBox;
        private System.Windows.Forms.TextBox radiusTxtBox;
        private System.Windows.Forms.TextBox maxRadiusTxtBox;
        private System.Windows.Forms.TextBox intensityTxtBox;
        private System.Windows.Forms.TextBox blurCountTxtBox;
        private System.Windows.Forms.TextBox biasTxtBox;
        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button applyBtn;
    }
}