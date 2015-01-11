namespace Game_Physics_Editor
{
    partial class PhysicsEditorForm
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
            this.bbListBox = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.minPosX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.minPosY = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.minPosZ = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.maxPosZ = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.maxPosX = new System.Windows.Forms.TextBox();
            this.maxPosY = new System.Windows.Forms.TextBox();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.wireframeCheckBox = new System.Windows.Forms.CheckBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bbListBox
            // 
            this.bbListBox.FormattingEnabled = true;
            this.bbListBox.Location = new System.Drawing.Point(13, 13);
            this.bbListBox.Name = "bbListBox";
            this.bbListBox.Size = new System.Drawing.Size(188, 238);
            this.bbListBox.TabIndex = 0;
            this.bbListBox.SelectedIndexChanged += new System.EventHandler(this.bbListBox_SelectedIndexChanged);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(22, 257);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 1;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(65, 286);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // minPosX
            // 
            this.minPosX.Location = new System.Drawing.Point(37, 19);
            this.minPosX.Name = "minPosX";
            this.minPosX.Size = new System.Drawing.Size(100, 20);
            this.minPosX.TabIndex = 3;
            this.minPosX.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Y:";
            // 
            // minPosY
            // 
            this.minPosY.Location = new System.Drawing.Point(37, 45);
            this.minPosY.Name = "minPosY";
            this.minPosY.Size = new System.Drawing.Size(100, 20);
            this.minPosY.TabIndex = 5;
            this.minPosY.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.minPosZ);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.minPosX);
            this.groupBox1.Controls.Add(this.minPosY);
            this.groupBox1.Location = new System.Drawing.Point(207, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Min BB Pos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "X:";
            // 
            // minPosZ
            // 
            this.minPosZ.Location = new System.Drawing.Point(37, 72);
            this.minPosZ.Name = "minPosZ";
            this.minPosZ.Size = new System.Drawing.Size(100, 20);
            this.minPosZ.TabIndex = 6;
            this.minPosZ.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.maxPosZ);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.maxPosX);
            this.groupBox2.Controls.Add(this.maxPosY);
            this.groupBox2.Location = new System.Drawing.Point(207, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Max BB Pos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Z:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "X:";
            // 
            // maxPosZ
            // 
            this.maxPosZ.Location = new System.Drawing.Point(37, 72);
            this.maxPosZ.Name = "maxPosZ";
            this.maxPosZ.Size = new System.Drawing.Size(100, 20);
            this.maxPosZ.TabIndex = 6;
            this.maxPosZ.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Y:";
            // 
            // maxPosX
            // 
            this.maxPosX.Location = new System.Drawing.Point(37, 19);
            this.maxPosX.Name = "maxPosX";
            this.maxPosX.Size = new System.Drawing.Size(100, 20);
            this.maxPosX.TabIndex = 3;
            this.maxPosX.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // maxPosY
            // 
            this.maxPosY.Location = new System.Drawing.Point(37, 45);
            this.maxPosY.Name = "maxPosY";
            this.maxPosY.Size = new System.Drawing.Size(100, 20);
            this.maxPosY.TabIndex = 5;
            this.maxPosY.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(103, 257);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 9;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // wireframeCheckBox
            // 
            this.wireframeCheckBox.AutoSize = true;
            this.wireframeCheckBox.Location = new System.Drawing.Point(244, 234);
            this.wireframeCheckBox.Name = "wireframeCheckBox";
            this.wireframeCheckBox.Size = new System.Drawing.Size(112, 17);
            this.wireframeCheckBox.TabIndex = 10;
            this.wireframeCheckBox.Text = "Render wire frame";
            this.wireframeCheckBox.UseVisualStyleBackColor = true;
            this.wireframeCheckBox.CheckedChanged += new System.EventHandler(this.wireframeCheckBox_CheckedChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(244, 257);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 11;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // PhysicsEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 325);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.wireframeCheckBox);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.bbListBox);
            this.Name = "PhysicsEditorForm";
            this.Text = "PhysicsEditorForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox bbListBox;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.TextBox minPosX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox minPosY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox minPosZ;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox maxPosZ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox maxPosX;
        private System.Windows.Forms.TextBox maxPosY;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.CheckBox wireframeCheckBox;
        private System.Windows.Forms.Button saveBtn;
    }
}