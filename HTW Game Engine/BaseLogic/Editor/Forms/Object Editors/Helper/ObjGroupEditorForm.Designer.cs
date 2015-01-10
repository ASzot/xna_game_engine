namespace BaseLogic.Editor.Forms
{
    partial class ObjGroupEditorForm
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
            this.objListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.applyPositionBtn = new System.Windows.Forms.Button();
            this.posXTxtBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.posYTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.posZTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.applyRotationBtn = new System.Windows.Forms.Button();
            this.rotXTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rotYTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rotZTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.applyScaleBtn = new System.Windows.Forms.Button();
            this.scaleXYZTxtBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.deleteGroupBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.centralRotAngleTxtBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.centralRotAngleBtn = new System.Windows.Forms.Button();
            this.centralRotXTxtBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.centralRotYTxtBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.centralRotZTxtBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.copyGroupBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // objListBox
            // 
            this.objListBox.FormattingEnabled = true;
            this.objListBox.Location = new System.Drawing.Point(172, 12);
            this.objListBox.Name = "objListBox";
            this.objListBox.Size = new System.Drawing.Size(153, 342);
            this.objListBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.applyPositionBtn);
            this.groupBox1.Controls.Add(this.posXTxtBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.posYTxtBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.posZTxtBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 128);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Offset Position";
            // 
            // applyPositionBtn
            // 
            this.applyPositionBtn.Location = new System.Drawing.Point(11, 97);
            this.applyPositionBtn.Name = "applyPositionBtn";
            this.applyPositionBtn.Size = new System.Drawing.Size(120, 23);
            this.applyPositionBtn.TabIndex = 18;
            this.applyPositionBtn.Text = "Apply";
            this.applyPositionBtn.UseVisualStyleBackColor = true;
            this.applyPositionBtn.Click += new System.EventHandler(this.applyPositionBtn_Click);
            // 
            // posXTxtBox
            // 
            this.posXTxtBox.Location = new System.Drawing.Point(31, 19);
            this.posXTxtBox.Name = "posXTxtBox";
            this.posXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posXTxtBox.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "X:";
            // 
            // posYTxtBox
            // 
            this.posYTxtBox.Location = new System.Drawing.Point(31, 45);
            this.posYTxtBox.Name = "posYTxtBox";
            this.posYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posYTxtBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Y:";
            // 
            // posZTxtBox
            // 
            this.posZTxtBox.Location = new System.Drawing.Point(31, 71);
            this.posZTxtBox.Name = "posZTxtBox";
            this.posZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.posZTxtBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Z:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.applyRotationBtn);
            this.groupBox2.Controls.Add(this.rotXTxtBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rotYTxtBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rotZTxtBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 128);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Offset Rotation";
            // 
            // applyRotationBtn
            // 
            this.applyRotationBtn.Location = new System.Drawing.Point(11, 97);
            this.applyRotationBtn.Name = "applyRotationBtn";
            this.applyRotationBtn.Size = new System.Drawing.Size(120, 23);
            this.applyRotationBtn.TabIndex = 18;
            this.applyRotationBtn.Text = "Apply";
            this.applyRotationBtn.UseVisualStyleBackColor = true;
            this.applyRotationBtn.Click += new System.EventHandler(this.applyRotationBtn_Click);
            // 
            // rotXTxtBox
            // 
            this.rotXTxtBox.Location = new System.Drawing.Point(31, 19);
            this.rotXTxtBox.Name = "rotXTxtBox";
            this.rotXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotXTxtBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "X:";
            // 
            // rotYTxtBox
            // 
            this.rotYTxtBox.Location = new System.Drawing.Point(31, 45);
            this.rotYTxtBox.Name = "rotYTxtBox";
            this.rotYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotYTxtBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Y:";
            // 
            // rotZTxtBox
            // 
            this.rotZTxtBox.Location = new System.Drawing.Point(31, 71);
            this.rotZTxtBox.Name = "rotZTxtBox";
            this.rotZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.rotZTxtBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Z:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.applyScaleBtn);
            this.groupBox3.Controls.Add(this.scaleXYZTxtBox);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(172, 366);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 69);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Set Scaling";
            // 
            // applyScaleBtn
            // 
            this.applyScaleBtn.Location = new System.Drawing.Point(25, 45);
            this.applyScaleBtn.Name = "applyScaleBtn";
            this.applyScaleBtn.Size = new System.Drawing.Size(120, 23);
            this.applyScaleBtn.TabIndex = 18;
            this.applyScaleBtn.Text = "Apply";
            this.applyScaleBtn.UseVisualStyleBackColor = true;
            this.applyScaleBtn.Click += new System.EventHandler(this.applyScaleBtn_Click);
            // 
            // scaleXYZTxtBox
            // 
            this.scaleXYZTxtBox.Location = new System.Drawing.Point(45, 19);
            this.scaleXYZTxtBox.Name = "scaleXYZTxtBox";
            this.scaleXYZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.scaleXYZTxtBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "XYZ:";
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(251, 451);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 52;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // deleteGroupBtn
            // 
            this.deleteGroupBtn.Location = new System.Drawing.Point(2, 441);
            this.deleteGroupBtn.Name = "deleteGroupBtn";
            this.deleteGroupBtn.Size = new System.Drawing.Size(89, 33);
            this.deleteGroupBtn.TabIndex = 53;
            this.deleteGroupBtn.Text = "Delete Group";
            this.deleteGroupBtn.UseVisualStyleBackColor = true;
            this.deleteGroupBtn.Click += new System.EventHandler(this.deleteGroupBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.centralRotAngleTxtBox);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.centralRotAngleBtn);
            this.groupBox4.Controls.Add(this.centralRotXTxtBox);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.centralRotYTxtBox);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.centralRotZTxtBox);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(12, 281);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(157, 153);
            this.groupBox4.TabIndex = 52;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Central Rotation";
            // 
            // centralRotAngleTxtBox
            // 
            this.centralRotAngleTxtBox.Location = new System.Drawing.Point(50, 97);
            this.centralRotAngleTxtBox.Name = "centralRotAngleTxtBox";
            this.centralRotAngleTxtBox.Size = new System.Drawing.Size(100, 20);
            this.centralRotAngleTxtBox.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Angle:";
            // 
            // centralRotAngleBtn
            // 
            this.centralRotAngleBtn.Location = new System.Drawing.Point(6, 123);
            this.centralRotAngleBtn.Name = "centralRotAngleBtn";
            this.centralRotAngleBtn.Size = new System.Drawing.Size(120, 23);
            this.centralRotAngleBtn.TabIndex = 18;
            this.centralRotAngleBtn.Text = "Apply";
            this.centralRotAngleBtn.UseVisualStyleBackColor = true;
            this.centralRotAngleBtn.Click += new System.EventHandler(this.centralRotBtn_Click);
            // 
            // centralRotXTxtBox
            // 
            this.centralRotXTxtBox.Location = new System.Drawing.Point(31, 19);
            this.centralRotXTxtBox.Name = "centralRotXTxtBox";
            this.centralRotXTxtBox.Size = new System.Drawing.Size(100, 20);
            this.centralRotXTxtBox.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "X:";
            // 
            // centralRotYTxtBox
            // 
            this.centralRotYTxtBox.Location = new System.Drawing.Point(31, 45);
            this.centralRotYTxtBox.Name = "centralRotYTxtBox";
            this.centralRotYTxtBox.Size = new System.Drawing.Size(100, 20);
            this.centralRotYTxtBox.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Y:";
            // 
            // centralRotZTxtBox
            // 
            this.centralRotZTxtBox.Location = new System.Drawing.Point(31, 71);
            this.centralRotZTxtBox.Name = "centralRotZTxtBox";
            this.centralRotZTxtBox.Size = new System.Drawing.Size(100, 20);
            this.centralRotZTxtBox.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Z:";
            // 
            // copyGroupBtn
            // 
            this.copyGroupBtn.Location = new System.Drawing.Point(97, 441);
            this.copyGroupBtn.Name = "copyGroupBtn";
            this.copyGroupBtn.Size = new System.Drawing.Size(89, 33);
            this.copyGroupBtn.TabIndex = 54;
            this.copyGroupBtn.Text = "Copy Group";
            this.copyGroupBtn.UseVisualStyleBackColor = true;
            this.copyGroupBtn.Click += new System.EventHandler(this.copyGroupBtn_Click);
            // 
            // ObjGroupEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(343, 487);
            this.ControlBox = false;
            this.Controls.Add(this.copyGroupBtn);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.deleteGroupBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.objListBox);
            this.Name = "ObjGroupEditorForm";
            this.Text = "Group Object Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox objListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button applyPositionBtn;
        private System.Windows.Forms.TextBox posXTxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox posYTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox posZTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button applyRotationBtn;
        private System.Windows.Forms.TextBox rotXTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rotYTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rotZTxtBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button applyScaleBtn;
        private System.Windows.Forms.TextBox scaleXYZTxtBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button deleteGroupBtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox centralRotAngleTxtBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button centralRotAngleBtn;
        private System.Windows.Forms.TextBox centralRotXTxtBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox centralRotYTxtBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox centralRotZTxtBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button copyGroupBtn;
    }
}