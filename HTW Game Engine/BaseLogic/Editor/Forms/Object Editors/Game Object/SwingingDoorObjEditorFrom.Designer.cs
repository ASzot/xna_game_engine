namespace BaseLogic.Editor.Forms
{
    partial class SwingingDoorObjEditorForm
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
            this.positionGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.zAxisRotTxtBox = new System.Windows.Forms.TextBox();
            this.yAxisRotTxtBox = new System.Windows.Forms.TextBox();
            this.xAxisRotTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openAngleTxtBox = new System.Windows.Forms.TextBox();
            this.closedAngleTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.swingSpeedTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.zOffsetTxtBox = new System.Windows.Forms.TextBox();
            this.yOffsetTxtBox = new System.Windows.Forms.TextBox();
            this.xOffsetTxtBox = new System.Windows.Forms.TextBox();
            this.positionGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(234, 181);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(69, 24);
            this.closeBtn.TabIndex = 20;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // positionGroupBox
            // 
            this.positionGroupBox.Controls.Add(this.label4);
            this.positionGroupBox.Controls.Add(this.label3);
            this.positionGroupBox.Controls.Add(this.label2);
            this.positionGroupBox.Controls.Add(this.zAxisRotTxtBox);
            this.positionGroupBox.Controls.Add(this.yAxisRotTxtBox);
            this.positionGroupBox.Controls.Add(this.xAxisRotTxtBox);
            this.positionGroupBox.Location = new System.Drawing.Point(9, 9);
            this.positionGroupBox.Name = "positionGroupBox";
            this.positionGroupBox.Size = new System.Drawing.Size(144, 100);
            this.positionGroupBox.TabIndex = 21;
            this.positionGroupBox.TabStop = false;
            this.positionGroupBox.Text = "Rotation Axis";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Z:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X:";
            // 
            // zAxisRotTxtBox
            // 
            this.zAxisRotTxtBox.Location = new System.Drawing.Point(35, 73);
            this.zAxisRotTxtBox.Name = "zAxisRotTxtBox";
            this.zAxisRotTxtBox.Size = new System.Drawing.Size(100, 20);
            this.zAxisRotTxtBox.TabIndex = 2;
            this.zAxisRotTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // yAxisRotTxtBox
            // 
            this.yAxisRotTxtBox.Location = new System.Drawing.Point(35, 46);
            this.yAxisRotTxtBox.Name = "yAxisRotTxtBox";
            this.yAxisRotTxtBox.Size = new System.Drawing.Size(100, 20);
            this.yAxisRotTxtBox.TabIndex = 1;
            this.yAxisRotTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // xAxisRotTxtBox
            // 
            this.xAxisRotTxtBox.Location = new System.Drawing.Point(35, 19);
            this.xAxisRotTxtBox.Name = "xAxisRotTxtBox";
            this.xAxisRotTxtBox.Size = new System.Drawing.Size(100, 20);
            this.xAxisRotTxtBox.TabIndex = 0;
            this.xAxisRotTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 116);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Open Angle:";
            // 
            // openAngleTxtBox
            // 
            this.openAngleTxtBox.Location = new System.Drawing.Point(146, 114);
            this.openAngleTxtBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.openAngleTxtBox.Name = "openAngleTxtBox";
            this.openAngleTxtBox.Size = new System.Drawing.Size(100, 20);
            this.openAngleTxtBox.TabIndex = 23;
            this.openAngleTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // closedAngleTxtBox
            // 
            this.closedAngleTxtBox.Location = new System.Drawing.Point(146, 135);
            this.closedAngleTxtBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.closedAngleTxtBox.Name = "closedAngleTxtBox";
            this.closedAngleTxtBox.Size = new System.Drawing.Size(100, 20);
            this.closedAngleTxtBox.TabIndex = 25;
            this.closedAngleTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 137);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Closed Angle:";
            // 
            // swingSpeedTxtBox
            // 
            this.swingSpeedTxtBox.Location = new System.Drawing.Point(146, 156);
            this.swingSpeedTxtBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.swingSpeedTxtBox.Name = "swingSpeedTxtBox";
            this.swingSpeedTxtBox.Size = new System.Drawing.Size(100, 20);
            this.swingSpeedTxtBox.TabIndex = 27;
            this.swingSpeedTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 158);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Swing Speed:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.zOffsetTxtBox);
            this.groupBox1.Controls.Add(this.yOffsetTxtBox);
            this.groupBox1.Controls.Add(this.xOffsetTxtBox);
            this.groupBox1.Location = new System.Drawing.Point(159, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 100);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotation Offset";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Z:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Y:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "X:";
            // 
            // zOffsetTxtBox
            // 
            this.zOffsetTxtBox.Location = new System.Drawing.Point(35, 73);
            this.zOffsetTxtBox.Name = "zOffsetTxtBox";
            this.zOffsetTxtBox.Size = new System.Drawing.Size(100, 20);
            this.zOffsetTxtBox.TabIndex = 2;
            this.zOffsetTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // yOffsetTxtBox
            // 
            this.yOffsetTxtBox.Location = new System.Drawing.Point(35, 46);
            this.yOffsetTxtBox.Name = "yOffsetTxtBox";
            this.yOffsetTxtBox.Size = new System.Drawing.Size(100, 20);
            this.yOffsetTxtBox.TabIndex = 1;
            this.yOffsetTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // xOffsetTxtBox
            // 
            this.xOffsetTxtBox.Location = new System.Drawing.Point(35, 19);
            this.xOffsetTxtBox.Name = "xOffsetTxtBox";
            this.xOffsetTxtBox.Size = new System.Drawing.Size(100, 20);
            this.xOffsetTxtBox.TabIndex = 0;
            this.xOffsetTxtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // DoorObjEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 212);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.swingSpeedTxtBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.closedAngleTxtBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.openAngleTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.positionGroupBox);
            this.Controls.Add(this.closeBtn);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DoorObjEditorForm";
            this.Text = "Door Editor";
            this.positionGroupBox.ResumeLayout(false);
            this.positionGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.GroupBox positionGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox zAxisRotTxtBox;
        private System.Windows.Forms.TextBox yAxisRotTxtBox;
        private System.Windows.Forms.TextBox xAxisRotTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox openAngleTxtBox;
        private System.Windows.Forms.TextBox closedAngleTxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox swingSpeedTxtBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox zOffsetTxtBox;
        private System.Windows.Forms.TextBox yOffsetTxtBox;
        private System.Windows.Forms.TextBox xOffsetTxtBox;
    }
}