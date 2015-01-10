namespace BaseLogic.Editor.Forms.Object_Editors
{
    partial class SoundEffectEditorForm
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
            this.soundFilenameTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pitchTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.volumeTxtBox = new System.Windows.Forms.TextBox();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(210, 161);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(112, 35);
            this.closeBtn.TabIndex = 15;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // soundFilenameTxtBox
            // 
            this.soundFilenameTxtBox.Location = new System.Drawing.Point(147, 6);
            this.soundFilenameTxtBox.Name = "soundFilenameTxtBox";
            this.soundFilenameTxtBox.Size = new System.Drawing.Size(134, 26);
            this.soundFilenameTxtBox.TabIndex = 16;
            this.soundFilenameTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Sound Filename:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Volume:";
            // 
            // pitchTxtBox
            // 
            this.pitchTxtBox.Location = new System.Drawing.Point(147, 70);
            this.pitchTxtBox.Name = "pitchTxtBox";
            this.pitchTxtBox.Size = new System.Drawing.Size(134, 26);
            this.pitchTxtBox.TabIndex = 18;
            this.pitchTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 21;
            this.label3.Text = "Pan:";
            // 
            // panTxtBox
            // 
            this.panTxtBox.Location = new System.Drawing.Point(147, 38);
            this.panTxtBox.Name = "panTxtBox";
            this.panTxtBox.Size = new System.Drawing.Size(134, 26);
            this.panTxtBox.TabIndex = 20;
            this.panTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "Pitch:";
            // 
            // volumeTxtBox
            // 
            this.volumeTxtBox.Location = new System.Drawing.Point(147, 102);
            this.volumeTxtBox.Name = "volumeTxtBox";
            this.volumeTxtBox.Size = new System.Drawing.Size(134, 26);
            this.volumeTxtBox.TabIndex = 22;
            this.volumeTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // acceptBtn
            // 
            this.acceptBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.acceptBtn.Location = new System.Drawing.Point(13, 161);
            this.acceptBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(112, 35);
            this.acceptBtn.TabIndex = 24;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // SoundEffectEditorForm
            // 
            this.AcceptButton = this.acceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(335, 210);
            this.ControlBox = false;
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.volumeTxtBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pitchTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.soundFilenameTxtBox);
            this.Controls.Add(this.closeBtn);
            this.Name = "SoundEffectEditorForm";
            this.Text = "Sound Effect Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.TextBox soundFilenameTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pitchTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox panTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox volumeTxtBox;
        private System.Windows.Forms.Button acceptBtn;
    }
}