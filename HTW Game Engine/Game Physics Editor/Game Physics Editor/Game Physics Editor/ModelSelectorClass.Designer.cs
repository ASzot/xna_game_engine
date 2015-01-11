namespace Game_Physics_Editor
{
    partial class ModelSelectorClass
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
            this.filenameTxtBox = new System.Windows.Forms.TextBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.saveLocationTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // filenameTxtBox
            // 
            this.filenameTxtBox.Location = new System.Drawing.Point(138, 20);
            this.filenameTxtBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.filenameTxtBox.Name = "filenameTxtBox";
            this.filenameTxtBox.Size = new System.Drawing.Size(224, 26);
            this.filenameTxtBox.TabIndex = 0;
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(110, 94);
            this.loadBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(156, 60);
            this.loadBtn.TabIndex = 1;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filename:";
            // 
            // saveLocationTxtBox
            // 
            this.saveLocationTxtBox.Location = new System.Drawing.Point(138, 55);
            this.saveLocationTxtBox.Name = "saveLocationTxtBox";
            this.saveLocationTxtBox.Size = new System.Drawing.Size(224, 26);
            this.saveLocationTxtBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Save Location:";
            // 
            // ModelSelectorClass
            // 
            this.AcceptButton = this.loadBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 172);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.saveLocationTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.filenameTxtBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ModelSelectorClass";
            this.Text = "Model Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filenameTxtBox;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox saveLocationTxtBox;
        private System.Windows.Forms.Label label2;
    }
}