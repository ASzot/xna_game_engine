namespace BaseLogic.Editor.Forms
{
    partial class SceneSettingsForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.ambientRTxtBox = new System.Windows.Forms.TextBox();
            this.ambientGTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ambientBTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ambientATxtBox = new System.Windows.Forms.TextBox();
            this.useSkymapCheckBox = new System.Windows.Forms.CheckBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.renderLinesCheckBox = new System.Windows.Forms.CheckBox();
            this.renderQuadsCheckBox = new System.Windows.Forms.CheckBox();
            this.renderTextCheckBox = new System.Windows.Forms.CheckBox();
            this.renderCullingBBCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ambientRTxtBox);
            this.groupBox1.Controls.Add(this.ambientGTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ambientBTxtBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ambientATxtBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ambient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "G:";
            // 
            // ambientRTxtBox
            // 
            this.ambientRTxtBox.Location = new System.Drawing.Point(31, 17);
            this.ambientRTxtBox.Name = "ambientRTxtBox";
            this.ambientRTxtBox.Size = new System.Drawing.Size(100, 20);
            this.ambientRTxtBox.TabIndex = 1;
            this.ambientRTxtBox.TextChanged += new System.EventHandler(this.txtBoxTextChanged);
            // 
            // ambientGTxtBox
            // 
            this.ambientGTxtBox.Location = new System.Drawing.Point(31, 43);
            this.ambientGTxtBox.Name = "ambientGTxtBox";
            this.ambientGTxtBox.Size = new System.Drawing.Size(100, 20);
            this.ambientGTxtBox.TabIndex = 3;
            this.ambientGTxtBox.TextChanged += new System.EventHandler(this.txtBoxTextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "B:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "R:";
            // 
            // ambientBTxtBox
            // 
            this.ambientBTxtBox.Location = new System.Drawing.Point(31, 69);
            this.ambientBTxtBox.Name = "ambientBTxtBox";
            this.ambientBTxtBox.Size = new System.Drawing.Size(100, 20);
            this.ambientBTxtBox.TabIndex = 5;
            this.ambientBTxtBox.TextChanged += new System.EventHandler(this.txtBoxTextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "A:";
            // 
            // ambientATxtBox
            // 
            this.ambientATxtBox.Location = new System.Drawing.Point(31, 95);
            this.ambientATxtBox.Name = "ambientATxtBox";
            this.ambientATxtBox.Size = new System.Drawing.Size(100, 20);
            this.ambientATxtBox.TabIndex = 7;
            this.ambientATxtBox.TextChanged += new System.EventHandler(this.txtBoxTextChanged);
            // 
            // useSkymapCheckBox
            // 
            this.useSkymapCheckBox.AutoSize = true;
            this.useSkymapCheckBox.Location = new System.Drawing.Point(12, 15);
            this.useSkymapCheckBox.Name = "useSkymapCheckBox";
            this.useSkymapCheckBox.Size = new System.Drawing.Size(86, 17);
            this.useSkymapCheckBox.TabIndex = 1;
            this.useSkymapCheckBox.Text = "Use Skymap";
            this.useSkymapCheckBox.UseVisualStyleBackColor = true;
            this.useSkymapCheckBox.CheckedChanged += new System.EventHandler(this.checkBoxCheckChanged);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(74, 281);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(84, 37);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // renderLinesCheckBox
            // 
            this.renderLinesCheckBox.AutoSize = true;
            this.renderLinesCheckBox.Location = new System.Drawing.Point(12, 38);
            this.renderLinesCheckBox.Name = "renderLinesCheckBox";
            this.renderLinesCheckBox.Size = new System.Drawing.Size(89, 17);
            this.renderLinesCheckBox.TabIndex = 3;
            this.renderLinesCheckBox.Text = "Render Lines";
            this.renderLinesCheckBox.UseVisualStyleBackColor = true;
            this.renderLinesCheckBox.CheckedChanged += new System.EventHandler(this.checkBoxCheckChanged);
            // 
            // renderQuadsCheckBox
            // 
            this.renderQuadsCheckBox.AutoSize = true;
            this.renderQuadsCheckBox.Location = new System.Drawing.Point(12, 61);
            this.renderQuadsCheckBox.Name = "renderQuadsCheckBox";
            this.renderQuadsCheckBox.Size = new System.Drawing.Size(95, 17);
            this.renderQuadsCheckBox.TabIndex = 4;
            this.renderQuadsCheckBox.Text = "Render Quads";
            this.renderQuadsCheckBox.UseVisualStyleBackColor = true;
            this.renderQuadsCheckBox.CheckedChanged += new System.EventHandler(this.checkBoxCheckChanged);
            // 
            // renderTextCheckBox
            // 
            this.renderTextCheckBox.AutoSize = true;
            this.renderTextCheckBox.Location = new System.Drawing.Point(12, 84);
            this.renderTextCheckBox.Name = "renderTextCheckBox";
            this.renderTextCheckBox.Size = new System.Drawing.Size(85, 17);
            this.renderTextCheckBox.TabIndex = 5;
            this.renderTextCheckBox.Text = "Render Text";
            this.renderTextCheckBox.UseVisualStyleBackColor = true;
            this.renderTextCheckBox.CheckedChanged += new System.EventHandler(this.checkBoxCheckChanged);
            // 
            // renderCullingBBCheckBox
            // 
            this.renderCullingBBCheckBox.AutoSize = true;
            this.renderCullingBBCheckBox.Location = new System.Drawing.Point(12, 107);
            this.renderCullingBBCheckBox.Name = "renderCullingBBCheckBox";
            this.renderCullingBBCheckBox.Size = new System.Drawing.Size(175, 17);
            this.renderCullingBBCheckBox.TabIndex = 6;
            this.renderCullingBBCheckBox.Text = "Render Culling Bounding Boxes";
            this.renderCullingBBCheckBox.UseVisualStyleBackColor = true;
            this.renderCullingBBCheckBox.CheckedChanged += new System.EventHandler(this.checkBoxCheckChanged);
            // 
            // SceneSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 333);
            this.ControlBox = false;
            this.Controls.Add(this.renderCullingBBCheckBox);
            this.Controls.Add(this.renderTextCheckBox);
            this.Controls.Add(this.renderQuadsCheckBox);
            this.Controls.Add(this.renderLinesCheckBox);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.useSkymapCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "SceneSettingsForm";
            this.Text = "Scene Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ambientRTxtBox;
        private System.Windows.Forms.TextBox ambientGTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ambientBTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ambientATxtBox;
        private System.Windows.Forms.CheckBox useSkymapCheckBox;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.CheckBox renderLinesCheckBox;
        private System.Windows.Forms.CheckBox renderQuadsCheckBox;
        private System.Windows.Forms.CheckBox renderTextCheckBox;
        private System.Windows.Forms.CheckBox renderCullingBBCheckBox;
    }
}