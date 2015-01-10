namespace RenderingSystem.Graphics.Forms
{
    partial class AASettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.countTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edgeThresholdMinTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.edgeThresholdTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.subPixelTxtBox = new System.Windows.Forms.TextBox();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Count:";
            // 
            // countTxtBox
            // 
            this.countTxtBox.Location = new System.Drawing.Point(234, 18);
            this.countTxtBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.countTxtBox.Name = "countTxtBox";
            this.countTxtBox.Size = new System.Drawing.Size(148, 26);
            this.countTxtBox.TabIndex = 1;
            this.countTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sub Pixel Aliasing Removal:";
            // 
            // edgeThresholdMinTxtBox
            // 
            this.edgeThresholdMinTxtBox.Location = new System.Drawing.Point(234, 138);
            this.edgeThresholdMinTxtBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edgeThresholdMinTxtBox.Name = "edgeThresholdMinTxtBox";
            this.edgeThresholdMinTxtBox.Size = new System.Drawing.Size(148, 26);
            this.edgeThresholdMinTxtBox.TabIndex = 5;
            this.edgeThresholdMinTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Edge Threshold:";
            // 
            // edgeThresholdTxtBox
            // 
            this.edgeThresholdTxtBox.Location = new System.Drawing.Point(234, 98);
            this.edgeThresholdTxtBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edgeThresholdTxtBox.Name = "edgeThresholdTxtBox";
            this.edgeThresholdTxtBox.Size = new System.Drawing.Size(148, 26);
            this.edgeThresholdTxtBox.TabIndex = 7;
            this.edgeThresholdTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 143);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Edge Threshold Minimum:";
            // 
            // subPixelTxtBox
            // 
            this.subPixelTxtBox.Location = new System.Drawing.Point(234, 58);
            this.subPixelTxtBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.subPixelTxtBox.Name = "subPixelTxtBox";
            this.subPixelTxtBox.Size = new System.Drawing.Size(148, 26);
            this.subPixelTxtBox.TabIndex = 9;
            this.subPixelTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Location = new System.Drawing.Point(240, 194);
            this.enabledCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.Size = new System.Drawing.Size(94, 24);
            this.enabledCheckBox.TabIndex = 14;
            this.enabledCheckBox.Text = "Enabled";
            this.enabledCheckBox.UseVisualStyleBackColor = true;
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(334, 251);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(112, 35);
            this.closeBtn.TabIndex = 15;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // AASettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 313);
            this.ControlBox = false;
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.enabledCheckBox);
            this.Controls.Add(this.subPixelTxtBox);
            this.Controls.Add(this.edgeThresholdTxtBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.edgeThresholdMinTxtBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.countTxtBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AASettingsForm";
            this.Text = "Anti Aliasing Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox countTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edgeThresholdMinTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edgeThresholdTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox subPixelTxtBox;
        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.Button closeBtn;
    }
}