namespace BaseLogic.Editor.Forms.Object_Editors
{
    partial class TimedParticleSystemEditorForm
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
            this.aliveTimeTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.setLightBtn = new System.Windows.Forms.Button();
            this.useLightCheckBox = new System.Windows.Forms.CheckBox();
            this.particleSystemComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.xPosTxtBox = new System.Windows.Forms.TextBox();
            this.zPosTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.yPosTxtBox = new System.Windows.Forms.TextBox();
            this.customizeBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(181, 251);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 15;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // aliveTimeTxtBox
            // 
            this.aliveTimeTxtBox.Location = new System.Drawing.Point(111, 27);
            this.aliveTimeTxtBox.Margin = new System.Windows.Forms.Padding(2);
            this.aliveTimeTxtBox.Name = "aliveTimeTxtBox";
            this.aliveTimeTxtBox.Size = new System.Drawing.Size(84, 20);
            this.aliveTimeTxtBox.TabIndex = 16;
            this.aliveTimeTxtBox.TextChanged += new System.EventHandler(this.aliveTimeTxtBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Milliseconds Alive:";
            // 
            // acceptBtn
            // 
            this.acceptBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptBtn.Location = new System.Drawing.Point(12, 251);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(75, 23);
            this.acceptBtn.TabIndex = 21;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // setLightBtn
            // 
            this.setLightBtn.Location = new System.Drawing.Point(111, 52);
            this.setLightBtn.Name = "setLightBtn";
            this.setLightBtn.Size = new System.Drawing.Size(75, 23);
            this.setLightBtn.TabIndex = 22;
            this.setLightBtn.Text = "Set Light";
            this.setLightBtn.UseVisualStyleBackColor = true;
            this.setLightBtn.Click += new System.EventHandler(this.setLightBtn_Click);
            // 
            // useLightCheckBox
            // 
            this.useLightCheckBox.AutoSize = true;
            this.useLightCheckBox.Location = new System.Drawing.Point(34, 56);
            this.useLightCheckBox.Name = "useLightCheckBox";
            this.useLightCheckBox.Size = new System.Drawing.Size(71, 17);
            this.useLightCheckBox.TabIndex = 23;
            this.useLightCheckBox.Text = "Use Light";
            this.useLightCheckBox.UseVisualStyleBackColor = true;
            this.useLightCheckBox.CheckedChanged += new System.EventHandler(this.useLightCheckBox_CheckedChanged);
            // 
            // particleSystemComboBox
            // 
            this.particleSystemComboBox.FormattingEnabled = true;
            this.particleSystemComboBox.Location = new System.Drawing.Point(111, 96);
            this.particleSystemComboBox.Name = "particleSystemComboBox";
            this.particleSystemComboBox.Size = new System.Drawing.Size(145, 21);
            this.particleSystemComboBox.TabIndex = 24;
            this.particleSystemComboBox.SelectedIndexChanged += new System.EventHandler(this.particleSystemComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Particle System:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.xPosTxtBox);
            this.groupBox1.Controls.Add(this.zPosTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.yPosTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(73, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 100);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
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
            this.xPosTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // zPosTxtBox
            // 
            this.zPosTxtBox.Location = new System.Drawing.Point(31, 71);
            this.zPosTxtBox.Name = "zPosTxtBox";
            this.zPosTxtBox.Size = new System.Drawing.Size(100, 20);
            this.zPosTxtBox.TabIndex = 11;
            this.zPosTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
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
            this.yPosTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // customizeBtn
            // 
            this.customizeBtn.Location = new System.Drawing.Point(111, 123);
            this.customizeBtn.Name = "customizeBtn";
            this.customizeBtn.Size = new System.Drawing.Size(75, 23);
            this.customizeBtn.TabIndex = 27;
            this.customizeBtn.Text = "Customize";
            this.customizeBtn.UseVisualStyleBackColor = true;
            this.customizeBtn.Click += new System.EventHandler(this.customizeBtn_Click);
            // 
            // TimedParticleSystemEditorForm
            // 
            this.AcceptButton = this.acceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(269, 286);
            this.ControlBox = false;
            this.Controls.Add(this.customizeBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.particleSystemComboBox);
            this.Controls.Add(this.useLightCheckBox);
            this.Controls.Add(this.setLightBtn);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aliveTimeTxtBox);
            this.Controls.Add(this.closeBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TimedParticleSystemEditorForm";
            this.Text = "Created Timed Particle System";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.TextBox aliveTimeTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.Button setLightBtn;
        private System.Windows.Forms.CheckBox useLightCheckBox;
        private System.Windows.Forms.ComboBox particleSystemComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox xPosTxtBox;
        private System.Windows.Forms.TextBox zPosTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox yPosTxtBox;
        private System.Windows.Forms.Button customizeBtn;
    }
}