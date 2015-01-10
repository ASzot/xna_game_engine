namespace BaseLogic.Editor.Forms
{
    partial class EditFlashingLightForm
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
            this.flashOutDurTxtBox = new System.Windows.Forms.TextBox();
            this.flashOutFreqTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flashOutDurTxtBox
            // 
            this.flashOutDurTxtBox.Location = new System.Drawing.Point(128, 16);
            this.flashOutDurTxtBox.Name = "flashOutDurTxtBox";
            this.flashOutDurTxtBox.Size = new System.Drawing.Size(100, 20);
            this.flashOutDurTxtBox.TabIndex = 6;
            this.flashOutDurTxtBox.TextChanged += new System.EventHandler(this.flashOutDurTxtBox_TextChanged);
            // 
            // flashOutFreqTxtBox
            // 
            this.flashOutFreqTxtBox.Location = new System.Drawing.Point(128, 39);
            this.flashOutFreqTxtBox.Name = "flashOutFreqTxtBox";
            this.flashOutFreqTxtBox.Size = new System.Drawing.Size(100, 20);
            this.flashOutFreqTxtBox.TabIndex = 7;
            this.flashOutFreqTxtBox.TextChanged += new System.EventHandler(this.flashOutFreqTxtBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Flash Out Duration:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Flash Out Frequency:";
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(160, 76);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 17;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // acceptBtn
            // 
            this.acceptBtn.Location = new System.Drawing.Point(9, 76);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(75, 23);
            this.acceptBtn.TabIndex = 18;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // EditFlashingLightForm
            // 
            this.AcceptButton = this.acceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(243, 108);
            this.ControlBox = false;
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flashOutFreqTxtBox);
            this.Controls.Add(this.flashOutDurTxtBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "EditFlashingLightForm";
            this.Text = "Edit Flashing Light";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox flashOutDurTxtBox;
        private System.Windows.Forms.TextBox flashOutFreqTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button acceptBtn;

    }
}