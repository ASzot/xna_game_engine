namespace Xna_Game_AI.Forms
{
    partial class AutoGenerateGraphForm
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
            this.nodeSpacingTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.xCenterTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.yCenterTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.zCenterTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.generateBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.rowsTxtBox = new System.Windows.Forms.TextBox();
            this.columnsTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nodeSpacingTxtBox
            // 
            this.nodeSpacingTxtBox.Location = new System.Drawing.Point(96, 6);
            this.nodeSpacingTxtBox.Name = "nodeSpacingTxtBox";
            this.nodeSpacingTxtBox.Size = new System.Drawing.Size(100, 20);
            this.nodeSpacingTxtBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.xCenterTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.yCenterTxtBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.zCenterTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(66, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Center Offset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "X:";
            // 
            // xCenterTxtBox
            // 
            this.xCenterTxtBox.Location = new System.Drawing.Point(30, 19);
            this.xCenterTxtBox.Name = "xCenterTxtBox";
            this.xCenterTxtBox.Size = new System.Drawing.Size(100, 20);
            this.xCenterTxtBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Y:";
            // 
            // yCenterTxtBox
            // 
            this.yCenterTxtBox.Location = new System.Drawing.Point(30, 45);
            this.yCenterTxtBox.Name = "yCenterTxtBox";
            this.yCenterTxtBox.Size = new System.Drawing.Size(100, 20);
            this.yCenterTxtBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Z:";
            // 
            // zCenterTxtBox
            // 
            this.zCenterTxtBox.Location = new System.Drawing.Point(30, 71);
            this.zCenterTxtBox.Name = "zCenterTxtBox";
            this.zCenterTxtBox.Size = new System.Drawing.Size(100, 20);
            this.zCenterTxtBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Node Spacing:";
            // 
            // generateBtn
            // 
            this.generateBtn.Location = new System.Drawing.Point(43, 209);
            this.generateBtn.Name = "generateBtn";
            this.generateBtn.Size = new System.Drawing.Size(75, 42);
            this.generateBtn.TabIndex = 8;
            this.generateBtn.Text = "Generate";
            this.generateBtn.UseVisualStyleBackColor = true;
            this.generateBtn.Click += new System.EventHandler(this.generateBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(167, 209);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 42);
            this.closeBtn.TabIndex = 9;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // rowsTxtBox
            // 
            this.rowsTxtBox.Location = new System.Drawing.Point(96, 147);
            this.rowsTxtBox.Name = "rowsTxtBox";
            this.rowsTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rowsTxtBox.TabIndex = 6;
            // 
            // columnsTxtBox
            // 
            this.columnsTxtBox.Location = new System.Drawing.Point(96, 174);
            this.columnsTxtBox.Name = "columnsTxtBox";
            this.columnsTxtBox.Size = new System.Drawing.Size(100, 20);
            this.columnsTxtBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Rows:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Columns:";
            // 
            // AutoGenerateGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 268);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.columnsTxtBox);
            this.Controls.Add(this.rowsTxtBox);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.generateBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nodeSpacingTxtBox);
            this.Name = "AutoGenerateGraphForm";
            this.Text = "Generate Graph";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nodeSpacingTxtBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox xCenterTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox yCenterTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox zCenterTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button generateBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.TextBox rowsTxtBox;
        private System.Windows.Forms.TextBox columnsTxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}