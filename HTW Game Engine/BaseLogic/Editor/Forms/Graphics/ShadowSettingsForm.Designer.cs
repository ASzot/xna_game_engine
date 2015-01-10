namespace BaseLogic.Editor.Forms
{
    partial class ShadowSettingsForm
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
            this.acceptBtn = new System.Windows.Forms.Button();
            this.cascadeResTxtBox = new System.Windows.Forms.TextBox();
            this.cascadeDivTxtBox = new System.Windows.Forms.TextBox();
            this.maxCascadeMapsTxtBox = new System.Windows.Forms.TextBox();
            this.maxSpotShadowMapsTxtBox = new System.Windows.Forms.TextBox();
            this.spotShadowMapResTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.applyBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // acceptBtn
            // 
            this.acceptBtn.Location = new System.Drawing.Point(95, 160);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(75, 23);
            this.acceptBtn.TabIndex = 0;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // cascadeResTxtBox
            // 
            this.cascadeResTxtBox.Location = new System.Drawing.Point(144, 12);
            this.cascadeResTxtBox.Name = "cascadeResTxtBox";
            this.cascadeResTxtBox.Size = new System.Drawing.Size(100, 20);
            this.cascadeResTxtBox.TabIndex = 1;
            // 
            // cascadeDivTxtBox
            // 
            this.cascadeDivTxtBox.Location = new System.Drawing.Point(144, 38);
            this.cascadeDivTxtBox.Name = "cascadeDivTxtBox";
            this.cascadeDivTxtBox.Size = new System.Drawing.Size(100, 20);
            this.cascadeDivTxtBox.TabIndex = 2;
            // 
            // maxCascadeMapsTxtBox
            // 
            this.maxCascadeMapsTxtBox.Location = new System.Drawing.Point(144, 64);
            this.maxCascadeMapsTxtBox.Name = "maxCascadeMapsTxtBox";
            this.maxCascadeMapsTxtBox.Size = new System.Drawing.Size(100, 20);
            this.maxCascadeMapsTxtBox.TabIndex = 3;
            // 
            // maxSpotShadowMapsTxtBox
            // 
            this.maxSpotShadowMapsTxtBox.Location = new System.Drawing.Point(144, 90);
            this.maxSpotShadowMapsTxtBox.Name = "maxSpotShadowMapsTxtBox";
            this.maxSpotShadowMapsTxtBox.Size = new System.Drawing.Size(100, 20);
            this.maxSpotShadowMapsTxtBox.TabIndex = 4;
            // 
            // spotShadowMapResTxtBox
            // 
            this.spotShadowMapResTxtBox.Location = new System.Drawing.Point(144, 116);
            this.spotShadowMapResTxtBox.Name = "spotShadowMapResTxtBox";
            this.spotShadowMapResTxtBox.Size = new System.Drawing.Size(100, 20);
            this.spotShadowMapResTxtBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cascade Res:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Cascade Divisions:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Max Spot Shadow Maps:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Max Cascade Maps:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Spot Shadow Map Res:";
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(95, 190);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 23);
            this.applyBtn.TabIndex = 11;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(95, 220);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 12;
            this.cancelBtn.Text = "Exit";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // ShadowSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 263);
            this.ControlBox = false;
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spotShadowMapResTxtBox);
            this.Controls.Add(this.maxSpotShadowMapsTxtBox);
            this.Controls.Add(this.maxCascadeMapsTxtBox);
            this.Controls.Add(this.cascadeDivTxtBox);
            this.Controls.Add(this.cascadeResTxtBox);
            this.Controls.Add(this.acceptBtn);
            this.Name = "ShadowSettingsForm";
            this.Text = "Shadow Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.TextBox cascadeResTxtBox;
        private System.Windows.Forms.TextBox cascadeDivTxtBox;
        private System.Windows.Forms.TextBox maxCascadeMapsTxtBox;
        private System.Windows.Forms.TextBox maxSpotShadowMapsTxtBox;
        private System.Windows.Forms.TextBox spotShadowMapResTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}