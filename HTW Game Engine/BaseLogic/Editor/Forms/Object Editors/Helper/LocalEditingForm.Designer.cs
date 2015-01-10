namespace BaseLogic.Editor.Forms
{
    partial class LocalEditingForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.xPosTxtBox = new System.Windows.Forms.TextBox();
            this.zPosTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yPosTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.xPosTxtBox);
            this.groupBox1.Controls.Add(this.zPosTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.yPosTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(60, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Relative Positon";
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
            this.xPosTxtBox.TextChanged += new System.EventHandler(this.xPosTxtBox_TextChanged);
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
            // LocalEditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 285);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LocalEditingForm";
            this.Text = "LocalEditingForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox xPosTxtBox;
        private System.Windows.Forms.TextBox zPosTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox yPosTxtBox;
    }
}