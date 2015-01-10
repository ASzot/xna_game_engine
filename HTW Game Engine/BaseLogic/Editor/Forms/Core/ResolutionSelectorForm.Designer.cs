namespace BaseLogic.Editor.Forms.Core
{
    partial class ResolutionSelectorForm
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
            this.resWidthTxtBox = new System.Windows.Forms.TextBox();
            this.resHeightTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.createBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resWidthTxtBox
            // 
            this.resWidthTxtBox.Location = new System.Drawing.Point(57, 12);
            this.resWidthTxtBox.Name = "resWidthTxtBox";
            this.resWidthTxtBox.Size = new System.Drawing.Size(100, 20);
            this.resWidthTxtBox.TabIndex = 0;
            // 
            // resHeightTxtBox
            // 
            this.resHeightTxtBox.Location = new System.Drawing.Point(57, 38);
            this.resHeightTxtBox.Name = "resHeightTxtBox";
            this.resHeightTxtBox.Size = new System.Drawing.Size(100, 20);
            this.resHeightTxtBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Width:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height:";
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(57, 64);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(100, 53);
            this.createBtn.TabIndex = 4;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // ResolutionSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 136);
            this.ControlBox = false;
            this.Controls.Add(this.createBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resHeightTxtBox);
            this.Controls.Add(this.resWidthTxtBox);
            this.Name = "ResolutionSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resolution Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox resWidthTxtBox;
        private System.Windows.Forms.TextBox resHeightTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button createBtn;
    }
}