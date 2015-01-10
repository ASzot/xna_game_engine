namespace BaseLogic.Editor.Forms
{
    partial class TranslatingDoorObjEditorForm
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
            this.translateSpeedTxtBox = new System.Windows.Forms.TextBox();
            this.closeHeightTxtBox = new System.Windows.Forms.TextBox();
            this.openHeightTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // translateSpeedTxtBox
            // 
            this.translateSpeedTxtBox.Location = new System.Drawing.Point(101, 12);
            this.translateSpeedTxtBox.Name = "translateSpeedTxtBox";
            this.translateSpeedTxtBox.Size = new System.Drawing.Size(100, 20);
            this.translateSpeedTxtBox.TabIndex = 0;
            this.translateSpeedTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // closeHeightTxtBox
            // 
            this.closeHeightTxtBox.Location = new System.Drawing.Point(101, 39);
            this.closeHeightTxtBox.Name = "closeHeightTxtBox";
            this.closeHeightTxtBox.Size = new System.Drawing.Size(100, 20);
            this.closeHeightTxtBox.TabIndex = 1;
            this.closeHeightTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // openHeightTxtBox
            // 
            this.openHeightTxtBox.Location = new System.Drawing.Point(101, 66);
            this.openHeightTxtBox.Name = "openHeightTxtBox";
            this.openHeightTxtBox.Size = new System.Drawing.Size(100, 20);
            this.openHeightTxtBox.TabIndex = 2;
            this.openHeightTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Translate Speed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Closed Height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Open Height:";
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(126, 109);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 6;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // TranslatingDoorObjEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 144);
            this.ControlBox = false;
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.openHeightTxtBox);
            this.Controls.Add(this.closeHeightTxtBox);
            this.Controls.Add(this.translateSpeedTxtBox);
            this.Name = "TranslatingDoorObjEditorForm";
            this.Text = "Translating Door Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox translateSpeedTxtBox;
        private System.Windows.Forms.TextBox closeHeightTxtBox;
        private System.Windows.Forms.TextBox openHeightTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button closeBtn;
    }
}